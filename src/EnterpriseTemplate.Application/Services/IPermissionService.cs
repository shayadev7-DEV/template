namespace EnterpriseTemplate.Application.Services;

/// <summary>
/// Service responsible for permission lookup and assignment.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Determines whether a user has the requested permission.
    /// </summary>
    Task<bool> UserHasPermissionAsync(Guid userId, string permissionCode, CancellationToken cancellationToken);
}
