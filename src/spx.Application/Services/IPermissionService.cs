namespace spx.Application.Services;

/// <summary>
/// Application-layer contract for resolving permission grants for authenticated users.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Determines whether the authenticated Identity user has the requested permission code through role membership.
    /// </summary>
    Task<bool> HasPermissionAsync(Guid userId, string permissionCode, CancellationToken cancellationToken);
}
