namespace EnterpriseTemplate.Shared.Options;

/// <summary>
/// Database configuration options.
/// </summary>
public sealed class DatabaseOptions
{
    /// <summary>
    /// Gets or sets the default SQL Server connection string.
    /// </summary>
    public string DefaultConnection { get; set; } = string.Empty;
}
