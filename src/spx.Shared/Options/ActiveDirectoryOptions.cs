namespace spx.Shared.Options;

/// <summary>
/// Active Directory integration options.
/// </summary>
public sealed class ActiveDirectoryOptions
{
    /// <summary>
    /// Gets or sets the AD domain.
    /// </summary>
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the LDAP path.
    /// </summary>
    public string LdapPath { get; set; } = string.Empty;
}
