using EnterpriseTemplate.Application.Abstractions;
using EnterpriseTemplate.Domain.Authorization;
using EnterpriseTemplate.Domain.Common;
using EnterpriseTemplate.Domain.Users;
using EnterpriseTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTemplate.Persistence;

/// <summary>
/// EF Core database context that hosts Identity and domain entities.
/// </summary>
public sealed class ApplicationDbContext : IdentityDbContext<IdentityApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly ICurrentUserService? _currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService? currentUserService = null) : base(options)
    {
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Gets the users aggregate set.
    /// </summary>
    public DbSet<ApplicationUser> DomainUsers => Set<ApplicationUser>();

    /// <summary>
    /// Gets roles.
    /// </summary>
    public DbSet<Role> DomainRoles => Set<Role>();

    /// <summary>
    /// Gets permissions.
    /// </summary>
    public DbSet<Permission> Permissions => Set<Permission>();

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();

        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private void ApplyAuditInformation()
    {
        string? userName = _currentUserService?.UserName;
        DateTimeOffset now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userName;
                entry.Entity.CreatedDate = now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedBy = userName;
                entry.Entity.ModifiedDate = now;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedBy = userName;
                entry.Entity.DeletedDate = now;
            }
        }
    }
}
