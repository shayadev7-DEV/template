using EnterpriseTemplate.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace EnterpriseTemplate.Infrastructure.Caching;

/// <summary>
/// In-process cache service suitable for single-node or non-critical cached data.
/// </summary>
public sealed class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryCacheService"/> class.
    /// </summary>
    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <inheritdoc />
    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan absoluteExpiration, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(key, out T? value) && value is not null)
        {
            return value;
        }

        value = await factory(cancellationToken).ConfigureAwait(false);
        _memoryCache.Set(key, value, absoluteExpiration);

        return value;
    }

    /// <inheritdoc />
    public Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        _memoryCache.Remove(key);

        return Task.CompletedTask;
    }
}
