using FlightTicket.Domain.Messages;
using MediatR;

namespace FlightTicket.Domain.Interfaces.MediatR;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
{
}
