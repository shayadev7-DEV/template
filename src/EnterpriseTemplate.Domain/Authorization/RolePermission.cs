using EnterpriseTemplate.Domain.Common;

namespace EnterpriseTemplate.Domain.Authorization;

/// <summary>
/// Join entity connecting roles and permissions.
/// </summary>
public sealed class RolePermission : AuditableEntity
{
    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the permission identifier.
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public Role? Role { get; set; }

    /// <summary>
    /// Gets or sets the permission.
    /// </summary>
    public Permission? Permission { get; set; }
}
