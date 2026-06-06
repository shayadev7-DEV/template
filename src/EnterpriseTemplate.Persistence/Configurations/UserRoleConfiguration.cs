using EnterpriseTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplate.Persistence.Configurations;

/// <summary>
/// EF Core configuration for user-role assignments.
/// </summary>
public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles", "Domain");
        builder.HasKey(item => item.Id);
        builder.HasQueryFilter(item => !item.IsDeleted);
        builder.HasIndex(item => new { item.UserId, item.RoleId }).IsUnique();
        builder.HasOne(item => item.User).WithMany("UserRoles").HasForeignKey(item => item.UserId);
        builder.HasOne(item => item.Role).WithMany().HasForeignKey(item => item.RoleId);
    }
}
