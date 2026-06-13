namespace spx.Shared.Options;

/// <summary>
/// Cache configuration options.
/// </summary>
public sealed class CacheOptions
{
    /// <summary>
    /// Gets or sets the cache provider name.
    /// </summary>
    public string Provider { get; set; } = "Distributed";
}
