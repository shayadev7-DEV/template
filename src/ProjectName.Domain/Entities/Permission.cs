namespace ProjectName.Domain.Entities;

/// <summary>Permission aggregate member representing a granular secured capability.</summary>
/// <remarks>Used by role and dynamic authorization policy handlers.</remarks>
public sealed class Permission : AuditableEntity
{
    private Permission() { Code = string.Empty; Name = string.Empty; }
    private Permission(string code, string name, PermissionType type) { Code = code; Name = name; Type = type; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public PermissionType Type { get; private set; }
    public static Permission Create(string code, string name, PermissionType type)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainRuleException("Permission code is required.");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainRuleException("Permission name is required.");
        return new Permission(code.Trim().ToUpperInvariant(), name.Trim(), type);
    }
}
