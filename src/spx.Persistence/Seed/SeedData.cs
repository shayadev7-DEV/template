using spx.Domain.Authorization;
using spx.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace spx.Persistence.Seed;

/// <summary>
/// Seeds permissions and bootstrap Identity roles used as permission containers.
/// </summary>
public static class SeedData
{
    private const string AdministratorRoleName = "Administrator";

    private static readonly (string Name, string Code, PermissionType Type)[] PermissionSeeds =
    [
        ("Users Read", "users.read", PermissionType.Read),
        ("Users Create", "users.create", PermissionType.Create),
        ("Users Update", "users.update", PermissionType.Update),
        ("Users Delete", "users.delete", PermissionType.Delete),
        ("Permissions Manage", "permissions.manage", PermissionType.Execute)
    ];

    /// <summary>
    /// Applies idempotent seed data.
    /// </summary>
    public static async Task SeedAsync(
        ApplicationDbContext dbContext,
        RoleManager<IdentityRole<Guid>> roleManager,
        CancellationToken cancellationToken)
    {
        foreach ((string name, string code, PermissionType type) in PermissionSeeds)
        {
            if (!await dbContext.Permissions.AnyAsync(permission => permission.Code.ToLower() == code, cancellationToken).ConfigureAwait(false))
            {
                await dbContext.Permissions.AddAsync(new Permission(name, code, type), cancellationToken).ConfigureAwait(false);
            }
        }

        IdentityRole<Guid>? administratorRole = await roleManager.FindByNameAsync(AdministratorRoleName).ConfigureAwait(false);
        if (administratorRole is null)
        {
            administratorRole = new IdentityRole<Guid>(AdministratorRoleName);
            IdentityResult result = await roleManager.CreateAsync(administratorRole).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Unable to seed '{AdministratorRoleName}' Identity role: {errors}");
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        string[] permissionCodes = PermissionSeeds.Select(seed => seed.Code).ToArray();
        List<Guid> administratorPermissionIds = await dbContext.Permissions
            .Where(permission => permissionCodes.Contains(permission.Code.ToLower()))
            .Select(permission => permission.Id)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        foreach (Guid permissionId in administratorPermissionIds)
        {
            bool rolePermissionExists = await dbContext.RolePermissions
                .AnyAsync(item => item.RoleId == administratorRole.Id && item.PermissionId == permissionId, cancellationToken)
                .ConfigureAwait(false);

            if (!rolePermissionExists)
            {
                await dbContext.RolePermissions.AddAsync(new RolePermission
                {
                    RoleId = administratorRole.Id,
                    PermissionId = permissionId
                }, cancellationToken).ConfigureAwait(false);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
