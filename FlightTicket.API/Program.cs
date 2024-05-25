using FlightTicket.Application.Queries.Ticket;
using FlightTicket.Domain.Extension;
using FlightTicket.Infrastructure;
using FlightTicket.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .ConfigureInfrastructure(builder.Configuration);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen().
    Configure<ApiBehaviorOptions>(options =>
        options.SuppressModelStateInvalidFilter = true)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTicketListQuery).Assembly))
;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    DbInitializer.SeedData(app);
    app.UseSwagger().UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();
app.Run();
