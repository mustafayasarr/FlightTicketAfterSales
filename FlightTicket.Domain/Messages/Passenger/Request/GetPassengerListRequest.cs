using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Passenger.Response;

namespace FlightTicket.Domain.Messages.Passenger.Request;

public class GetPassengerListRequest : IQuery<List<GetPassengerListResponse>>
{
}
