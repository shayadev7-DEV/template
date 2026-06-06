namespace EnterpriseTemplate.Application.Services;

/// <summary>
/// Service responsible for role management use cases.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    Task AssignRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
