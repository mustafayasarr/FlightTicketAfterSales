using FlightTicket.Domain.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FlightTicket.Infrastructure.Persistence.Interceptor;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly DateTime _dateTimeNow = DateTime.Now;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public void UpdateEntities(DbContext? context)
    {
        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreationDate = _dateTimeNow;
                entry.Entity.IsDeleted = false;
                entry.Entity.IsActive = true;
            }

            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModificationDate = _dateTimeNow;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.IsActive = false;
                entry.Entity.DeletionDate = _dateTimeNow;
            }
        }
    }

}
