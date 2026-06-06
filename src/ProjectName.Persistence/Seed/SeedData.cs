namespace ProjectName.Persistence.Seed;
/// <summary>Seed data definitions for roles, permissions, and administrator bootstrap.</summary>
/// <remarks>Migration strategy: keep model migrations deterministic and call seed routines during deployment/startup migrations.</remarks>
public static class SeedData
{
    public static readonly string[] Permissions = ["Users.Read", "Users.Create", "Users.Update", "Users.Delete", "Roles.Manage", "Reports.Read"];
    public static readonly string[] Roles = [ApplicationConstants.AdminRole, "Manager", "Reader"];
}
