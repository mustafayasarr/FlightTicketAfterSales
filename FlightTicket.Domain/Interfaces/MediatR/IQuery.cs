using FlightTicket.Domain.Messages;
using MediatR;

namespace FlightTicket.Domain.Interfaces.MediatR;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseRequest
{
}
