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

}
