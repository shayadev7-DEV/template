using EnterpriseTemplate.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplate.Persistence.Configurations;

/// <summary>
/// EF Core configuration for role-permission assignments.
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
        builder.HasOne(item => item.Role).WithMany("RolePermissions").HasForeignKey(item => item.RoleId);
        builder.HasOne(item => item.Permission).WithMany().HasForeignKey(item => item.PermissionId);
    }
}
