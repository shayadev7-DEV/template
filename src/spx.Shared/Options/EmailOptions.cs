namespace spx.Shared.Options;

/// <summary>
/// Email delivery options.
/// </summary>
public sealed class EmailOptions
{
    /// <summary>
    /// Gets or sets the default sender address.
    /// </summary>
    public string Sender { get; set; } = string.Empty;
}
