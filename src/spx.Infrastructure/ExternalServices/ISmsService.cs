namespace spx.Infrastructure.ExternalServices;

/// <summary>
/// Sends SMS messages through a replaceable provider.
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Sends an SMS message asynchronously.
    /// </summary>
    Task SendAsync(string to, string message, CancellationToken cancellationToken);
}
