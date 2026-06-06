namespace ProjectName.Persistence.Identity;
/// <summary>ASP.NET Core Identity user record for authentication infrastructure.</summary>
/// <remarks>Kept in Persistence so the Domain user aggregate remains provider-agnostic.</remarks>
public sealed class ApplicationIdentityUser : IdentityUser<Guid>
{
    public Guid? DomainUserId { get; set; }
}
