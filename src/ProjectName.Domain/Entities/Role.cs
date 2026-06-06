namespace ProjectName.Domain.Entities;

/// <summary>Role aggregate root that groups permissions for assignment to users.</summary>
/// <remarks>Domain role is intentionally separate from IdentityRole to keep business authorization model independent.</remarks>
public sealed class Role : AggregateRoot
{
    private readonly List<RolePermission> _permissions = [];

    private Role()
    {
        Name = string.Empty;
        NormalizedName = string.Empty;
    }

    private Role(string name)
    {
        Name = name.Trim();
        NormalizedName = Name.ToUpperInvariant();
    }

    public string Name { get; private set; }

    public string NormalizedName { get; private set; }

    public IReadOnlyCollection<RolePermission> Permissions => _permissions.AsReadOnly();

    public static Role Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainRuleException("Role name is required.");
        }

        return new Role(name);
    }

    public void GrantPermission(Guid permissionId)
    {
        if (_permissions.Any(x => x.PermissionId == permissionId))
        {
            return;
        }

        _permissions.Add(RolePermission.Create(Id, permissionId));
    }
}
