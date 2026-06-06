using EnterpriseTemplate.Application.Abstractions;
using EnterpriseTemplate.Infrastructure.BackgroundJobs;
using EnterpriseTemplate.Infrastructure.Caching;
using EnterpriseTemplate.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseTemplate.Infrastructure;

/// <summary>
/// Registers infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure dependencies such as logging adapters, cache adapters, and hosted services.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            options.SchemaName = "dbo";
            options.TableName = "DistributedCache";
        });
        services.AddScoped<IApplicationLogger, ApplicationLogger>();
        services.AddScoped<MemoryCacheService>();
        services.AddScoped<DistributedCacheService>();
        services.AddScoped<ICacheService, DistributedCacheService>();
        services.AddHostedService<QueuedBackgroundService>();

        return services;
    }
}
