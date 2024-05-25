using FlightTicket.Domain.Models.Entities.Base;

namespace FlightTicket.Domain.Models.Entities;

public record class TicketEntity : BaseEntity
{
    public required Guid FlightId { get; set; }
    public required Guid PassengerId { get; set; }
    public string PNR { get; set; }
    public virtual PassengerEntity Passenger { get; set; }
    public virtual FlightEntity Flight { get; set; }
}
