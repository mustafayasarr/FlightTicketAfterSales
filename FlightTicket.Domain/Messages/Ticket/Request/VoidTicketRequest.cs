using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Ticket.Response;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class VoidTicketRequest:ICommand<VoidTicketResponse>
{
    public Guid TicketId { get; set; }
    public string PNR { get; set; }
}
