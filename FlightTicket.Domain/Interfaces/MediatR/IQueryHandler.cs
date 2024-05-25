using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages;
using MediatR;

namespace FlightTicket.Domain.Interfaces.MediaTr;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
