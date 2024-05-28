using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Airport.Response;
using FlightTicket.Domain.Models.Dtos;

namespace FlightTicket.Domain.Messages.Airport.Request;

public class GetAirportListRequest : IQuery<PaginatedList<GetAirportListResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Find { get; set; }

}
