namespace ProjectName.Infrastructure.Caching;

/// <summary>In-process cache adapter for IMemoryCache.</summary>
public sealed class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(cache.Get<T>(key));
    }

    public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default)
    {
        cache.Set(key, value, ttl);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        cache.Remove(key);

        return Task.CompletedTask;
    }
}
