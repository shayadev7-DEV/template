namespace ProjectName.Application.Abstractions.Authorization;

/// <summary>Provides permission lookup for policy-based and permission-based authorization.</summary>
public interface IPermissionService
{
    Task<bool> UserHasPermissionAsync(
        string userId,
        string permission,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
