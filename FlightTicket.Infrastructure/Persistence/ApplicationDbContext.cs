using FlightTicket.Domain.Models.Entities;
using FlightTicket.Domain.Models.Entities.Base;
using FlightTicket.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FlightTicket.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    
    public DbSet<AirportEntity> Airports => Set<AirportEntity>();
    public DbSet<FlightEntity> Flights => Set<FlightEntity>();
    public DbSet<PassengerEntity> Passengers => Set<PassengerEntity>();
    public DbSet<TicketEntity> Tickets => Set<TicketEntity>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override async void OnModelCreating(ModelBuilder builder)
    {
        Expression<Func<AuditableEntity, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var mutableEntityType in builder.Model.GetEntityTypes())
        {
            // check if current entity type is child of BaseModel
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(AuditableEntity)))
            {
                // modify expression to handle correct child type
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // set filter
                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
        }

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
