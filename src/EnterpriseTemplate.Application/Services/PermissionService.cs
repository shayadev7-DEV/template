namespace EnterpriseTemplate.Application.Services;

/// <summary>
/// Service responsible for permission checks.
/// </summary>
public sealed class PermissionService : IPermissionService
{
    /// <inheritdoc />
    public Task<bool> UserHasPermissionAsync(Guid userId, string permissionCode, CancellationToken cancellationToken)
    {
        return Task.FromResult(false);
    }
}
