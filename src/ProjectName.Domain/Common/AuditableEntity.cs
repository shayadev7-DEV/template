namespace ProjectName.Domain.Common;

/// <summary>Base entity with enterprise auditing and soft-delete fields.</summary>
/// <remarks>Auditing belongs to domain state; value assignment is coordinated by persistence interceptors.</remarks>
public abstract class AuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}
