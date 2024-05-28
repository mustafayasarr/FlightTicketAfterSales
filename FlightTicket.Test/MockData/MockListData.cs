using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Models.Entities;

namespace FlightTicket.Test.MockData;

public static class MockListData
{
    public static List<AirportEntity> AirportList()
    {
        return
         [
             new AirportEntity
        {
            Id =  Guid.Parse("96110cc1-eda7-4990-b75f-4dc87e6dada5"),
            AirportCode = "LTAF",
            AirportName = "Adana Airport",
            Location = "Adana",
            CreationDate = DateTime.Now,
            IsActive = true,
            IsDeleted = false
        },
        AirportsMockData.DestinationAirport(),
        AirportsMockData.OriginAirport()
         ];

    }

    public static List<FlightEntity> FlightList()
    {
        return [
            FlightMockData.DeleteFlight(),
            FlightMockData.VoidFlight(),
            FlightMockData.ReissueFlight()
            ];
    }
    public static List<TicketEntity> TicketList()
    {
        return [
            TicketMockData.VoidTicket(),
            TicketMockData.ReissueTicket()
     ];
    }
    public static List<PassengerEntity> PassengerList()
    {
        return [
            PassengerMockData.Passenger(),
            PassengerMockData.VoidPassenger(),
            PassengerMockData.ReissuePassenger()
     ];
    }
}
