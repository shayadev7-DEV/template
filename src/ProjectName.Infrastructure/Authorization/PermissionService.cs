namespace ProjectName.Infrastructure.Authorization;

/// <summary>Permission lookup service used by authorization handlers.</summary>
/// <remarks>Can be optimized with cache or claims projection without changing controllers.</remarks>
public sealed class PermissionService(ICacheService cache) : IPermissionService
{
    public async Task<bool> UserHasPermissionAsync(
        string userId,
        string permission,
        CancellationToken cancellationToken = default)
    {
        var permissions = await GetUserPermissionsAsync(userId, cancellationToken);

        return permissions.Contains(permission, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await cache.GetAsync<IReadOnlyCollection<string>>($"permissions:{userId}", cancellationToken)
            ?? Array.Empty<string>();
    }
}
