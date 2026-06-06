using System.Linq.Expressions;
using ProjectName.Domain.Entities;
using ProjectName.Persistence.Identity;

namespace ProjectName.Persistence;

/// <summary>EF Core DbContext containing domain aggregates and ASP.NET Core Identity tables.</summary>
/// <remarks>Persistence owns database mapping, global filters, soft delete, and auditing.</remarks>
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationIdentityUser, IdentityRole<Guid>, Guid>(options), IApplicationDbContext
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var entityType in builder.Model.GetEntityTypes()
                     .Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType)))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var filter = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(parameter, nameof(AuditableEntity.IsDeleted)),
                    Expression.Constant(false)),
                parameter);

            builder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAudit()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.CreatedBy ??= "system";
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDate = now;
                entry.Entity.ModifiedBy ??= "system";
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedDate = now;
                entry.Entity.DeletedBy ??= "system";
            }
        }
    }
}
