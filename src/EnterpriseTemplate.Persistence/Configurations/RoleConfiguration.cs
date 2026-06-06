using EnterpriseTemplate.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplate.Persistence.Configurations;

/// <summary>
/// EF Core configuration for roles.
/// </summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private const int NameMaxLength = 128;

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "Domain");
        builder.HasKey(role => role.Id);
        builder.HasQueryFilter(role => !role.IsDeleted);
        builder.Property(role => role.Name).HasMaxLength(NameMaxLength).IsRequired();
        builder.HasIndex(role => role.Name).IsUnique();
        builder.Navigation(role => role.RolePermissions).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Ignore(role => role.DomainEvents);
    }
}
