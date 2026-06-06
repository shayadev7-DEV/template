using EnterpriseTemplate.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplate.Persistence.Configurations;

/// <summary>
/// EF Core configuration for permissions.
/// </summary>
public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    private const int NameMaxLength = 128;
    private const int CodeMaxLength = 256;

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions", "Domain");
        builder.HasKey(permission => permission.Id);
        builder.HasQueryFilter(permission => !permission.IsDeleted);
        builder.Property(permission => permission.Name).HasMaxLength(NameMaxLength).IsRequired();
        builder.Property(permission => permission.Code).HasMaxLength(CodeMaxLength).IsRequired();
        builder.HasIndex(permission => permission.Code).IsUnique();
    }
}
