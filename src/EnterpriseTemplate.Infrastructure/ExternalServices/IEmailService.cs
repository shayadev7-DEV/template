namespace EnterpriseTemplate.Infrastructure.ExternalServices;

/// <summary>
/// Sends application emails through a provider selected outside the domain model.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken);
}
