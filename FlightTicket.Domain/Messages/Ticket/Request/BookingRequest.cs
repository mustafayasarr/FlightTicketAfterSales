using FlightTicket.Domain.Interfaces.MediatR;
using FlightTicket.Domain.Messages.Ticket.Response;

namespace FlightTicket.Domain.Messages.Ticket.Request;

public class BookingRequest:ICommand<BookingResponse>
{
    public Guid FlightId { get; set; }
    public Guid? PassengerId { get; set; }
    public bool IsNewPassenger { get; set; } = false;
    public string? PassengerName { get; set; }
    public string? PassengerLastName{ get; set; }
    public DateTime? BirthDate { get; set; }
}
