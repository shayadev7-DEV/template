using EnterpriseTemplate.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplate.Persistence.Authorization;

/// <summary>
/// EF Core permission resolver that maps Identity users to Identity roles and then to domain permissions.
/// </summary>
public sealed class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionService"/> class.
    /// </summary>
    public PermissionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<bool> HasPermissionAsync(Guid userId, string permissionCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(permissionCode))
        {
            return false;
        }

        string normalizedPermissionCode = permissionCode.Trim().ToLowerInvariant();

        return await (
            from userRole in _dbContext.UserRoles.AsNoTracking()
            join rolePermission in _dbContext.RolePermissions.AsNoTracking() on userRole.RoleId equals rolePermission.RoleId
            join permission in _dbContext.Permissions.AsNoTracking() on rolePermission.PermissionId equals permission.Id
            where userRole.UserId == userId && permission.Code.ToLower() == normalizedPermissionCode
            select permission.Id)
            .AnyAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
