namespace OSS.Domain.Entities;
public class AuditableEntity : BaseEntity
{
    public ApplicationUser CreatedBy { get; set; }
    public string CreatedById { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public ApplicationUser LastModifiedBy { get; set; }
    public string LastModifiedById { get; set; }

    public DateTime? LastModified { get; set; }
}