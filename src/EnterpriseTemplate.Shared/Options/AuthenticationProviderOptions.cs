namespace EnterpriseTemplate.Shared.Options;

/// <summary>
/// Hybrid authentication provider options.
/// </summary>
public sealed class AuthenticationProviderOptions
{
    /// <summary>
    /// Gets or sets the active authentication provider.
    /// </summary>
    public string ActiveProvider { get; set; } = "Identity";
}
