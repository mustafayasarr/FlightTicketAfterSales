using FlightTicket.Domain.Models.Dtos;

namespace FlightTicket.Domain.Messages.Flight.Response;

public class FindFlightResponse
{
    public Guid Id { get; set; }
    public required string AirlineName { get; set; }
    public required DateTime DepartureDateTime { get; set; }
    public required DateTime ArrivalDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AirportDto OriginAirport { get; set; }
    public AirportDto DestinationAirport { get; set; }
}
