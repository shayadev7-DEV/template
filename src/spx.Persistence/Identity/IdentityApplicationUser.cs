using Microsoft.AspNetCore.Identity;

namespace spx.Persistence.Identity;

/// <summary>
/// ASP.NET Core Identity user record linked to the domain user aggregate.
/// </summary>
public sealed class IdentityApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the linked domain user identifier.
    /// </summary>
    public Guid DomainUserId { get; set; }
}
