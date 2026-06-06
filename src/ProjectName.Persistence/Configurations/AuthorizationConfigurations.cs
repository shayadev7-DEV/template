using ProjectName.Domain.Entities;

namespace ProjectName.Persistence.Configurations;

/// <summary>EF Core mapping for role, permission, and join authorization entities.</summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "auth");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x => x.NormalizedName).HasMaxLength(128).IsRequired();
        builder.HasIndex(x => x.NormalizedName).IsUnique();
        builder.HasMany<RolePermission>("_permissions").WithOne().HasForeignKey(x => x.RoleId);
    }
}

/// <summary>EF Core mapping for permissions.</summary>
public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions", "auth");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(32);
        builder.HasIndex(x => x.Code).IsUnique();
    }
}

/// <summary>EF Core mapping for role-permission joins.</summary>
public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions", "auth");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();
    }
}

/// <summary>EF Core mapping for user-role joins.</summary>
public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles", "auth");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
    }
}
