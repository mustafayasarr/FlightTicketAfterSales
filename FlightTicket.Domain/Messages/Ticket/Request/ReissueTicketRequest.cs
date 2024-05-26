using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Ticket.Response;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class ReissueTicketRequest:ICommand<ReissueTicketResponse>
{
    public Guid PassengerId { get; set; }
    public required string PNR { get; set; }
    public Guid NewFlightId { get; set; }
}
