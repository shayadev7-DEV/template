using System.Reflection;
using ProjectName.Application.Common.Behaviors;
using ProjectName.Application.Mapping;
using ProjectName.Application.Services;
using ProjectName.Application.Validation;

namespace ProjectName.Application;

/// <summary>Registers Application layer services, mapping, validation, and CQRS behaviors.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
