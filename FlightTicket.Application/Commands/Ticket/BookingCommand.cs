using FlightTicket.Domain.Helpers;
using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages;
using FlightTicket.Domain.Messages.Ticket.Request;
using FlightTicket.Domain.Messages.Ticket.Response;
using FlightTicket.Domain.Models.Entities;
using FlightTicket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Application.Commands.Ticket;

public class BookingCommand(ApplicationDbContext context) : ICommandHandler<BookingRequest, BookingResponse>
{
    public async Task<Result<BookingResponse>> Handle(BookingRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsNewPassenger)
        {
            var passengerEntity = await context.Passengers.AsNoTracking().FirstOrDefaultAsync(a => a.Id == new Guid(request.PassengerId));
            if (passengerEntity == null)
                return Result.Fail<BookingResponse>("Yolcu kaydı bulunamadı");
        }
        else
        {
            PassengerEntity passengerEntity = new PassengerEntity()
            {
                FirstName = request.PassengerName,
                LastName = request.PassengerLastName,
                BirthDate = request.BirthDate.Value
            };
            await context.Passengers.AddAsync(passengerEntity);
            request.PassengerId = passengerEntity.Id.ToString();
        }


        var flightEntity = await context.Flights.AsNoTracking().FirstOrDefaultAsync(a => a.Id == new Guid(request.FlightId), cancellationToken);
        if (flightEntity == null)
            return Result.Fail<BookingResponse>("Uçuş bulunamadı.");

        TicketEntity ticketEntity = new TicketEntity
        {
            FlightId = new Guid(request.FlightId),
            PassengerId = new Guid(request.PassengerId),
            PNR = PNRGenerator.Generate()
        };

        context.Tickets.Add(ticketEntity);
        await context.SaveChangesAsync();
        return Result.Ok(new BookingResponse(ticketEntity.PNR));
    }
}
