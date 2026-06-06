namespace ProjectName.Domain.Enums;

/// <summary>Generic lifecycle status for auditable domain records.</summary>
public enum Status
{
    Draft = 0,
    Active = 1,
    Inactive = 2,
    Suspended = 3,
    Deleted = 4
}
