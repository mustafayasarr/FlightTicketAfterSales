using FlightTicket.Domain.Helpers;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Commands.Ticket;

public class ReissueTicketCommand(ApplicationDbContext context) : ICommandHandler<ReissueTicketRequest, ReissueTicketResponse>
{
    public async Task<Result<ReissueTicketResponse>> Handle(ReissueTicketRequest request, CancellationToken cancellationToken)
    {
        var getTicket = await context.Tickets.FirstOrDefaultAsync(a => a.PassengerId == request.PassengerId && a.PNR == request.PNR && a.IsActive);

        if (getTicket is null)
            return Result.Fail<ReissueTicketResponse>("Bilet bulunamadı.");

        var getFlight = await context.Flights.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.NewFlightId);

        if (getFlight is null)
            return Result.Fail<ReissueTicketResponse>("uçuş bulunamadı.");

        if (getFlight.DepartureDateTime < DateTime.Now)
            return Result.Fail<ReissueTicketResponse>("geçmiş tarihli uçuş.");

        context.Tickets.Remove(getTicket);

        TicketEntity ticketEntity = new()
        {
            FlightId = getFlight.Id,
            PassengerId = request.PassengerId,
            PNR = PNRGenerator.Generate()
        };

        await context.Tickets.AddAsync(ticketEntity);
        await context.SaveChangesAsync();

        return Result.Ok(new ReissueTicketResponse(ticketEntity.PNR));
    }
}
