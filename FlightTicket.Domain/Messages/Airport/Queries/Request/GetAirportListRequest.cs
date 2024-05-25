using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Airport.Queries.Response;

namespace FlightTicket.Domain.Messages.Airport.Queries.Request;

public class GetAirportListRequest:IQuery<PaginatedList<GetAirportListResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
