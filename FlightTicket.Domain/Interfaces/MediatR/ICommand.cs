using FlightTicket.Domain.Messages;
using MediatR;

namespace FlightTicket.Domain.Interfaces.MediatR;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseRequest, IBaseCommand
{
}
