using System.Text.Json;

namespace ProjectName.Infrastructure.Caching;
/// <summary>Distributed cache adapter for Redis or SQL Server distributed cache providers.</summary>
public sealed class DistributedCacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    { var bytes = await cache.GetStringAsync(key, cancellationToken); return bytes is null ? default : JsonSerializer.Deserialize<T>(bytes); }
    public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default) => cache.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = ttl }, cancellationToken);
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default) => cache.RemoveAsync(key, cancellationToken);
}
