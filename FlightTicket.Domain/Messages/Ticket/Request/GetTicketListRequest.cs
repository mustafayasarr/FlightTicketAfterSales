using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Ticket.Response;
using MediatR;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class GetTicketListRequest : IQuery<List<GetTicketListResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;

}
