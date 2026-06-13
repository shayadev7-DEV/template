namespace spx.Application.Abstractions;

/// <summary>
/// Cache abstraction implemented by memory and distributed cache adapters.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets a cached value or creates it when missing.
    /// </summary>
    Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan absoluteExpiration, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a cached value.
    /// </summary>
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}
