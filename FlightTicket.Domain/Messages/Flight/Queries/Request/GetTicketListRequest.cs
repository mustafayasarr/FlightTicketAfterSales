using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Flight.Queries.Response;
using MediatR;

namespace FlightTicket.Domain.Messages.Flight.Queries.Request;

public class GetTicketListRequest:IQuery<List<GetTicketListResponse>>
{
}
