using EnterpriseTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplate.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the domain user aggregate.
/// </summary>
public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private const int NameMaxLength = 100;
    private const int EmailMaxLength = 320;
    private const int MobileMaxLength = 20;

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", "Domain");
        builder.HasKey(user => user.Id);
        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.OwnsOne(user => user.Email, email =>
        {
            email.Property(item => item.Value).HasColumnName("Email").HasMaxLength(EmailMaxLength).IsRequired();
            email.HasIndex(item => item.Value).IsUnique();
        });

        builder.OwnsOne(user => user.FullName, fullName =>
        {
            fullName.Property(item => item.FirstName).HasColumnName("FirstName").HasMaxLength(NameMaxLength).IsRequired();
            fullName.Property(item => item.LastName).HasColumnName("LastName").HasMaxLength(NameMaxLength).IsRequired();
        });

        builder.OwnsOne(user => user.MobileNumber, mobile =>
        {
            mobile.Property(item => item.Value).HasColumnName("MobileNumber").HasMaxLength(MobileMaxLength);
        });

        builder.Ignore(user => user.DomainEvents);
    }
}
