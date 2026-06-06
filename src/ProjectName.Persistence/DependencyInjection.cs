using ProjectName.Persistence.Identity;

namespace ProjectName.Persistence;
/// <summary>Registers EF Core SQL Server context and ASP.NET Core Identity persistence.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));
        var db = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(db.ConnectionString, sql => sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)).EnableSensitiveDataLogging(db.EnableSensitiveDataLogging));
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddIdentity<ApplicationIdentityUser, IdentityRole<Guid>>(options => { options.Password.RequiredLength = 12; options.Password.RequireDigit = true; options.Password.RequireUppercase = true; options.Password.RequireLowercase = true; options.Password.RequireNonAlphanumeric = true; options.Lockout.MaxFailedAccessAttempts = 5; options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); options.SignIn.RequireConfirmedAccount = true; }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        return services;
    }
}
