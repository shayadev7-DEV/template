using spx.Domain.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace spx.Persistence.Configurations;

/// <summary>
/// EF Core configuration for permission grants assigned to ASP.NET Core Identity roles.
/// </summary>
public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions", "Domain");
        builder.HasKey(item => item.Id);
        builder.HasQueryFilter(item => !item.IsDeleted);
        builder.HasIndex(item => new { item.RoleId, item.PermissionId }).IsUnique();
        builder.HasOne<IdentityRole<Guid>>().WithMany().HasForeignKey(item => item.RoleId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(item => item.Permission).WithMany().HasForeignKey(item => item.PermissionId).OnDelete(DeleteBehavior.Cascade);
    }
}
