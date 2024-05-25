namespace FlightTicket.Domain.Models.Entities.Base;

public record class AuditableEntity
{
    public required DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public DateTime? DeletionDate { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}
