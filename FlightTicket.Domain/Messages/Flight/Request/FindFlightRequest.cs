using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Response;
using FlightTicket.Domain.Models.Dtos;

namespace FlightTicket.Domain.Messages.Flight.Request;

public class FindFlightRequest : ICommand<List<FindFlightResponse>>
{
    public Guid OriginAirportId { get; set; }
    public Guid DestinationAirportId { get; set; }
    public DateTime DepartureDate { get; set; }
    public short TotalPassengers { get; set; }
}
