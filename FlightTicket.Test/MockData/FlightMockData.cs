using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Models.Entities;

namespace FlightTicket.Test.MockData;

public class FlightMockData
{
    public static FlightEntity DeleteFlight()
    {
        return new FlightEntity
        {
            Id = Guid.Parse("f8aa20b1-c8ea-47fe-92fe-32f0d031f2fc"),
            AirlineName = Airlines.THY,
            DepartureDateTime = DateTime.Now.AddDays(10),
            ArrivalDateTime = DateTime.Now.AddDays(10).AddHours(2),
            OriginAirportId = AirportsMockData.OriginAirport().Id,
            OriginAirport = AirportsMockData.OriginAirport(),
            DestinationAirportId = AirportsMockData.DestinationAirport().Id,
            DestinationAirport = AirportsMockData.DestinationAirport(),
            CreationDate = DateTime.Now,
            IsActive = false,
            IsDeleted = true,
            DeletionDate= DateTime.Now
        };
    }
    public static FlightEntity VoidFlight()
    {
        return new FlightEntity
        {
            Id = Guid.Parse("f3d5a284-45c6-4e38-bef3-c698c5132436"),
            AirlineName = Airlines.Pegasus,
            DepartureDateTime = DateTime.Now.AddDays(10),
            ArrivalDateTime = DateTime.Now.AddDays(10).AddHours(2),
            OriginAirportId = AirportsMockData.OriginAirport().Id,
            OriginAirport = AirportsMockData.OriginAirport(),
            DestinationAirportId = AirportsMockData.DestinationAirport().Id,
            DestinationAirport = AirportsMockData.DestinationAirport(),
            CreationDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false,
        };
    }
    public static FlightEntity ReissueFlight()
    {
        return new FlightEntity
        {
            Id = Guid.Parse("abbeac66-0953-4845-bd4d-513b642e9154"),
            AirlineName = Airlines.AnadoluJet,
            DepartureDateTime = DateTime.Now.AddDays(4),
            ArrivalDateTime = DateTime.Now.AddDays(5).AddHours(2),
            OriginAirportId = AirportsMockData.OriginAirport().Id,
            OriginAirport = AirportsMockData.OriginAirport(),
            DestinationAirportId = AirportsMockData.DestinationAirport().Id,
            DestinationAirport = AirportsMockData.DestinationAirport(),
            CreationDate = DateTime.Now.AddDays(-1),
            IsActive = true,
            IsDeleted = false,
            ModificationDate=DateTime.Now
        };
    }

}
