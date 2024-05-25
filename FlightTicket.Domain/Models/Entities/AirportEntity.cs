using FlightTicket.Domain.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightTicket.Domain.Models.Entities
{
    [Table("Airports")]
    public record class AirportEntity:BaseEntity
    {
        public required string AirportCode { get; set; }
        public required string AirportName { get; set; }
        public required string Location { get; set; }
    }
}
