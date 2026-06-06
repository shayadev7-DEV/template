using EnterpriseTemplate.Domain.Authorization;
using EnterpriseTemplate.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplate.Persistence.Seed;

/// <summary>
/// Seeds roles, permissions, and bootstrap data.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Applies idempotent seed data.
    /// </summary>
    public static async Task SeedAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!await dbContext.Permissions.AnyAsync(cancellationToken).ConfigureAwait(false))
        {
            await dbContext.Permissions.AddRangeAsync(
                new Permission("Users Read", "Users.Read", PermissionType.Read),
                new Permission("Users Create", "Users.Create", PermissionType.Create),
                new Permission("Users Update", "Users.Update", PermissionType.Update),
                new Permission("Users Delete", "Users.Delete", PermissionType.Delete));
        }

        if (!await dbContext.DomainRoles.AnyAsync(cancellationToken).ConfigureAwait(false))
        {
            await dbContext.DomainRoles.AddAsync(new Role("Administrator"), cancellationToken).ConfigureAwait(false);
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
