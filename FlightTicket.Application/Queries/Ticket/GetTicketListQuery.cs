using FlightTicket.Domain.Interfaces.MediaTr;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Flight.Queries.Request;
using FlightTicket.Domain.Messages.Flight.Queries.Response;
using FlightTicket.Domain.Models.Dtos;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Queries.Ticket;

public class GetTicketListQuery : IQueryHandler<GetTicketListRequest, List<GetTicketListResponse>>
{
    private readonly ApplicationDbContext _context;
    public GetTicketListQuery(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<List<GetTicketListResponse>>> Handle(GetTicketListRequest request, CancellationToken cancellationToken)
    {
        var tickets = await _context.Tickets
            .AsNoTracking()
            .Include(x => x.Passenger)
            .Include(x => x.Flight)
            .ThenInclude(x => x.OriginAirport)
            .Include(x => x.Flight)
            .ThenInclude(x => x.DestinationAirport)
            .Where(a => a.IsActive)
            .Select(a => new GetTicketListResponse
            {
                Id = a.Id,
                PNR = a.PNR,
                Flight = new FlightDto
                {
                    AirlineName = a.Flight.AirlineName,
                    ArrivalDateTime = a.Flight.ArrivalDateTime,
                    DepartureDateTime = a.Flight.DepartureDateTime,
                    OriginAirport = new AirportDto
                    {
                        AirportCode = a.Flight.OriginAirport.AirportCode,
                        AirportName = a.Flight.OriginAirport.AirportName,
                        Location = a.Flight.OriginAirport.Location
                    },
                    DestinationAirport = new AirportDto
                    {
                        AirportCode = a.Flight.DestinationAirport.AirportCode,
                        AirportName = a.Flight.DestinationAirport.AirportName,
                        Location = a.Flight.DestinationAirport.Location
                    }
                },
                Passenger = new PassengerDto
                {
                    FirstName = a.Passenger.FirstName,
                    LastName = a.Passenger.LastName
                }
            }).ToListAsync();

        return Result.Ok(tickets);
    }
}
