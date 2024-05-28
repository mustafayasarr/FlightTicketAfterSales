using FlightTicket.Domain.Models.Entities;

namespace FlightTicket.Test.MockData;

public class AirportsMockData
{
    public static AirportEntity OriginAirport()
    {
        return new AirportEntity
        {
            Id = Guid.Parse("28807285-5664-4761-9c29-e37efcfb9c08"),
            AirportCode = "LTFM",
            AirportName = "Istanbul Airport",
            Location = "İstanbul",
            CreationDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false
        };
    }
    public static AirportEntity DestinationAirport()
    {
        return new AirportEntity
        {
            Id = Guid.Parse("b599d1f9-84cc-46c9-9852-9c4f5bad0fbd"),
            AirportCode = "LTAC",
            AirportName = "Ankara Esenboğa Airport",
            Location = "Ankara",
            CreationDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false
        };
    }

}
