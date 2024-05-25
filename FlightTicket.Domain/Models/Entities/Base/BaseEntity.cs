namespace FlightTicket.Domain.Models.Entities.Base;
public record class BaseEntity:AuditableEntity
{
    public Guid Id { get; set; }

}