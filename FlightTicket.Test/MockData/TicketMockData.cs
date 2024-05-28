using FlightTicket.Domain.Models.Entities;

namespace FlightTicket.Test.MockData;

public class TicketMockData
{
    public static TicketEntity VoidTicket() => new()
    {
        Id =  Guid.Parse("e6fd871d-ba7a-4518-9a8c-b0f322094a22"),
        FlightId = FlightMockData.VoidFlight().Id,
        Flight = FlightMockData.VoidFlight(),
        PassengerId = PassengerMockData.VoidPassenger().Id,
        Passenger = PassengerMockData.VoidPassenger(),
        CreationDate = new DateTime(),
        IsActive = true,
        IsDeleted = false,
        PNR = "TK-8C5SSFGC"
    };
    public static TicketEntity ReissueTicket()
    {
        return new TicketEntity
        {
            Id =  Guid.Parse("648eb398-75b9-4b6f-bd20-fb60d26fa0b4"),
            FlightId = FlightMockData.ReissueFlight().Id,
            Flight = FlightMockData.ReissueFlight(),
            PassengerId = PassengerMockData.ReissuePassenger().Id,
            Passenger = PassengerMockData.ReissuePassenger(),
            CreationDate = new DateTime(),
            IsActive = true,
            IsDeleted = false,
            PNR = "TK-8GJSVA5"
        };
    }
}
