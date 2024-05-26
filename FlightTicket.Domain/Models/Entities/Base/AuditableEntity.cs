namespace FlightTicket.Domain.Models.Entities.Base;

public record class AuditableEntity
{
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public DateTime? DeletionDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
