using Microsoft.AspNetCore.Authorization;
using ProjectName.Infrastructure.Authentication;
using ProjectName.Infrastructure.Authorization;
using ProjectName.Infrastructure.Caching;
using ProjectName.Infrastructure.Logging;
using ProjectName.Infrastructure.Persistence;

namespace ProjectName.Infrastructure;
/// <summary>Registers infrastructure adapters for logging, caching, auth providers, repositories, and Unit of Work.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationProvider, IdentityAuthenticationProvider>();
        services.AddScoped<IAuthenticationProvider, ActiveDirectoryAuthenticationProvider>();
        services.AddScoped<IAuthenticationProvider, EntraAuthenticationProvider>();
        services.AddScoped<IAuthenticationProvider, WindowsAuthenticationProvider>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ICacheService, MemoryCacheService>();
        services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        return services;
    }
}
