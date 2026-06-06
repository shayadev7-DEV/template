using EnterpriseTemplate.Domain.Authorization;
using EnterpriseTemplate.Domain.Common;

namespace EnterpriseTemplate.Domain.Users;

/// <summary>
/// Join entity connecting users and roles.
/// </summary>
public sealed class UserRole : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public ApplicationUser? User { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public Role? Role { get; set; }
}
