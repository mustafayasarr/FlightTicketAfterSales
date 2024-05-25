using FlightTicket.Domain.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightTicket.Domain.Models.Entities;

[Table("Flights")]
public record class FlightEntity : BaseEntity
{
    public required string AirlineName { get; set; }
    public required DateTime DepartureDateTime { get; set; }
    public required DateTime ArrivalDateTime { get; set; }
    public required Guid OriginAirportId { get; set; }
    public required Guid DestinationAirportId { get; set; }
    public virtual AirportEntity OriginAirport { get; set; }
    public virtual AirportEntity DestinationAirport { get; set; }
    public virtual ICollection<TicketEntity> Tickets { get; set; }

}
