namespace EnterpriseTemplate.Infrastructure.ExternalServices;

/// <summary>
/// Stores and retrieves application files.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Saves a stream and returns a storage key.
    /// </summary>
    Task<string> SaveAsync(Stream content, string fileName, CancellationToken cancellationToken);
}
