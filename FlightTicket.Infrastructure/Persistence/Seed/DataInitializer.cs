using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json.Nodes;

namespace FlightTicket.Infrastructure.Persistence.Seed;

public static class DbInitializer
{
    private static DateTime _createDate = DateTime.Now;
    public static void SeedData(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();
        context.AirportSeedData();
    }
    private static void AirportSeedData(this ApplicationDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        if (dbContext.Airports.Any()) return;
        dbContext.Database.EnsureCreated();

        using (WebClient wc = new WebClient())
        {
            dynamic json = JsonNode.Parse(wc.DownloadString("https://raw.githubusercontent.com/mwgg/Airports/master/airports.json"));
            foreach (var item in json)
            {
                AirportEntity entity = new()
                {
                    AirportName = item.Value["name"].ToString(),
                    AirportCode = item.Value["icao"].ToString(),
                    IsActive = true,
                    IsDeleted = false,
                    Location = item.Value["city"].ToString(),
                    CreationDate = _createDate
                };
                dbContext.Airports.Add(entity);
            }
        }
        dbContext.SaveChanges();
    }

    private static void FlightSeedData(this ApplicationDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        if (dbContext.Flights.Where(a => a.DepartureDateTime > DateTime.Now).Any())
            return;

        dbContext.Database.EnsureCreated();

        List<FlightEntity> entity = new()
        {
            new FlightEntity
            {
                Id = Guid.NewGuid(),
                AirlineName = Airlines.Pegasus,
                DepartureDateTime = DateTime.Now.AddDays(7),
                ArrivalDateTime = DateTime.Now.AddDays(7).AddHours(2),
                OriginAirportId = new Guid("18f5a6f1-dd86-4c72-a12e-e07c9b4ef1a1"),
                DestinationAirportId = new Guid("7ede0e97-b5b4-402a-85be-684267f8eef4"),
                CreationDate = _createDate,
                IsActive = true,
                IsDeleted = false,
            },
            new FlightEntity
            {
                Id = Guid.NewGuid(),
                AirlineName = Airlines.THY,
                DepartureDateTime = DateTime.Now.AddDays(4),
                ArrivalDateTime = DateTime.Now.AddDays(4).AddHours(1),
                OriginAirportId = new Guid("21ebbe8c-0375-4d79-a446-638ebdf6da75"),
                DestinationAirportId = new Guid("c1e9d51e-4b89-4f4a-b53e-0c5c4089f089"),
                CreationDate = _createDate,
                IsActive = true,
                IsDeleted = false,
            },
             new FlightEntity
            {
                Id = Guid.NewGuid(),
                AirlineName = Airlines.THY,
                DepartureDateTime = DateTime.Now.AddDays(10),
                ArrivalDateTime = DateTime.Now.AddDays(10).AddHours(3),
                OriginAirportId = new Guid("21ebbe8c-0375-4d79-a446-638ebdf6da75"),
                DestinationAirportId = new Guid("9c651396-1488-4a57-99ff-0a052b8a5053"),
                CreationDate = _createDate,
                IsActive = true,
                IsDeleted = false,
            },


        };
        dbContext.AddRange();
        entity.
    }
}
