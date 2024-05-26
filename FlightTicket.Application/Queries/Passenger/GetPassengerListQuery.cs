using FlightTicket.Domain.Interfaces.MediaTr;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Passenger.Request;
using FlightTicket.Domain.Messages.Passenger.Response;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Queries.Passenger;
public class GetPassengerListQuery(ApplicationDbContext context) : IQueryHandler<GetPassengerListRequest, List<GetPassengerListResponse>>
{
    public async Task<Result<List<GetPassengerListResponse>>> Handle(GetPassengerListRequest request, CancellationToken cancellationToken)
    {
        var getPassengers = await context.Passengers.AsNoTracking().Where(a => a.IsActive).Select(a => new GetPassengerListResponse
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Birthdate = a.BirthDate
        }).ToListAsync();

        return Result.Ok(getPassengers);
    }
}
