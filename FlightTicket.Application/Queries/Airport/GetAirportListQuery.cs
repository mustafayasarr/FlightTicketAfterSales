using FlightTicket.Domain.Interfaces.MediaTr;
using FlightTicket.Domain.Messages;
using FlightTicket.Infrastructure.Persistence;
using FlightTicket.Domain.Extension;
using FlightTicket.Domain.Messages.Airport.Request;
using FlightTicket.Domain.Messages.Airport.Response;

namespace FlightTicket.Application.Queries.Airport;

public class GetAirportListQuery : IQueryHandler<GetAirportListRequest, PaginatedList<GetAirportListResponse>>
{
    private readonly ApplicationDbContext _context;
    public GetAirportListQuery(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<PaginatedList<GetAirportListResponse>>> Handle(GetAirportListRequest request, CancellationToken cancellationToken)
    {
        var airports = await _context.Airports.Where(a => string.IsNullOrEmpty(request.Find) ? a.AirportName == a.AirportName : a.AirportName.ToLower().Contains(request.Find.ToLower())).OrderBy(a => a.AirportCode).Select(a => new GetAirportListResponse { Id = a.Id, AirportCode = a.AirportCode, AirportName = a.AirportName, Location = a.Location }).ToPaginationListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return Result.Ok(airports);
    }
}
