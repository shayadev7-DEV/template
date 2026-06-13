using System.Text.Json;
using spx.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace spx.Infrastructure.Caching;

/// <summary>
/// Distributed cache service for multi-node deployments.
/// </summary>
public sealed class DistributedCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="DistributedCacheService"/> class.
    /// </summary>
    public DistributedCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    /// <inheritdoc />
    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan absoluteExpiration, CancellationToken cancellationToken)
    {
        string? json = await _distributedCache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(json))
        {
            var cached = JsonSerializer.Deserialize<T>(json);

            if (cached is not null)
            {
                return cached;
            }
        }

        T value = await factory(cancellationToken).ConfigureAwait(false);
        DistributedCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration
        };
        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken).ConfigureAwait(false);

        return value;
    }

    /// <inheritdoc />
    public Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }
}
