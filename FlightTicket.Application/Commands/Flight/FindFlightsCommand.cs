using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Flight.Request;
using FlightTicket.Domain.Messages.Flight.Response;
using FlightTicket.Domain.Models.Dtos;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Commands.Flight;

public class FindFlightsCommand(ApplicationDbContext context) : ICommandHandler<FindFlightRequest, List<FindFlightResponse>>
{
    public async Task<Result<List<FindFlightResponse>>> Handle(FindFlightRequest request, CancellationToken cancellationToken)
    {
        TimeOnly startTime = new TimeOnly(0, 0, 0);
        TimeOnly endTime = new TimeOnly(23, 59, 59);
        var departureDate = new DateOnly(request.DepartureDate.Value.Year, request.DepartureDate.Value.Month, request.DepartureDate.Value.Day);

        var flights = await context.Flights.AsNoTracking().Include(a => a.OriginAirport).Include(a => a.DestinationAirport).Where(a => new DateTime(departureDate, startTime) <= a.DepartureDateTime && new DateTime(departureDate, endTime) >= a.DepartureDateTime && a.OriginAirportId == new Guid(request.OriginAirportId) && a.DestinationAirportId == new Guid(request.DestinationAirportId)).Select(a => new FindFlightResponse
        {
            Id = a.Id,
            AirlineName = a.AirlineName,
            DepartureDateTime = a.DepartureDateTime,
            ArrivalDateTime = a.ArrivalDateTime,
            Duration = a.ArrivalDateTime.Subtract(a.DepartureDateTime),
            DestinationAirport = new AirportDto(a.DestinationAirport.AirportCode, a.DestinationAirport.AirportName, a.DestinationAirport.Location),
            OriginAirport = new AirportDto(a.OriginAirport.AirportCode, a.OriginAirport.AirportName, a.OriginAirport.Location)
        }).ToListAsync(cancellationToken);

        return Result.Ok(flights);
    }
}
