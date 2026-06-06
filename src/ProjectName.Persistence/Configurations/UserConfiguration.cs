namespace ProjectName.Persistence.Configurations;
/// <summary>EF Core mapping for the ApplicationUser aggregate.</summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", "app");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.FullName, n => { n.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired(); n.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired(); });
        builder.OwnsOne(x => x.Email, e => e.Property(p => p.Value).HasColumnName("Email").HasMaxLength(256).IsRequired());
        builder.OwnsOne(x => x.MobileNumber, m => m.Property(p => p.Value).HasColumnName("MobileNumber").HasMaxLength(20).IsRequired());
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(x => x.UserType).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(x => x.Gender).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.HasMany<UserRole>("_roles").WithOne().HasForeignKey(x => x.UserId);
        builder.Navigation(x => x.Roles).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
