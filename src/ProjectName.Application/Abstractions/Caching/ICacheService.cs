namespace ProjectName.Application.Abstractions.Caching;
/// <summary>Cache abstraction supporting memory or distributed providers.</summary>
public interface ICacheService { Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default); Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default); Task RemoveAsync(string key, CancellationToken cancellationToken = default); }
