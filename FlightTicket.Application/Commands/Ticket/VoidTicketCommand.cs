using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Commands.Ticket;

public class VoidTicketCommand(ApplicationDbContext context) : ICommandHandler<VoidTicketRequest, VoidTicketResponse>
{
    public async Task<Result<VoidTicketResponse>> Handle(VoidTicketRequest request, CancellationToken cancellationToken)
    {
        var getTicket = await context.Tickets.Include(a => a.Flight).Where(a => a.Id == Guid.Parse(request.TicketId) && a.PNR == request.PNR && a.IsActive).FirstOrDefaultAsync();

        if (getTicket is null)
            return Result.Fail<VoidTicketResponse>(ErrorMessages.NoRecordTicketMessage, ErrorMessages.NoRecordTicketCode);

        if (getTicket.Flight.DepartureDateTime <= DateTime.Now)
            return Result.Fail<VoidTicketResponse>(ErrorMessages.FlightCompletedMessage, ErrorMessages.FlightCompletedCode);

        context.Tickets.Remove(getTicket);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new VoidTicketResponse());
    }
}
