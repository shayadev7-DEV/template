namespace EnterpriseTemplate.Shared.Options;

/// <summary>
/// JWT authentication options.
/// </summary>
public sealed class JwtOptions
{
    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the signing key.
    /// </summary>
    public string SigningKey { get; set; } = string.Empty;
}
