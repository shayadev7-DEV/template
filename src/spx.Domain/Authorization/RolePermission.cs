using spx.Domain.Common;

namespace spx.Domain.Authorization;

/// <summary>
/// Permission assignment for an authentication role.
/// </summary>
public sealed class RolePermission : AuditableEntity
{
    /// <summary>
    /// Gets or sets the ASP.NET Core Identity role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the permission identifier.
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// Gets or sets the permission granted to the Identity role.
    /// </summary>
    public Permission? Permission { get; set; }
}
