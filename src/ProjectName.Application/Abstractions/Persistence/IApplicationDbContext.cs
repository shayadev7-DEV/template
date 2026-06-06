namespace ProjectName.Application.Abstractions.Persistence;
/// <summary>Minimal EF Core context surface needed by application and infrastructure.</summary>
/// <remarks>Prevents direct dependency on the concrete persistence project.</remarks>
public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
