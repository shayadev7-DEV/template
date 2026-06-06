using EnterpriseTemplate.Domain.Common;

namespace EnterpriseTemplate.Domain.Authorization;

/// <summary>
/// Domain role aggregate used for permission assignments.
/// </summary>
public sealed class Role : AggregateRoot
{
    private readonly List<RolePermission> _rolePermissions = new();

    private Role()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    public Role(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the role name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets role permissions.
    /// </summary>
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();
}
