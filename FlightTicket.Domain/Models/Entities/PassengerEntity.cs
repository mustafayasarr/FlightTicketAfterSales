using FlightTicket.Domain.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightTicket.Domain.Models.Entities;

[Table("Passengers")]
public record class PassengerEntity:BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
}
