using System.Threading.RateLimiting;
using EnterpriseTemplate.Application;
using EnterpriseTemplate.Application.Abstractions;
using EnterpriseTemplate.Infrastructure;
using EnterpriseTemplate.Persistence;
using EnterpriseTemplate.Presentation;
using EnterpriseTemplate.Presentation.Authentication;
using EnterpriseTemplate.Presentation.Authorization;
using EnterpriseTemplate.Presentation.Filters;
using EnterpriseTemplate.Presentation.Middleware;
using EnterpriseTemplate.Shared.Options;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "EnterpriseTemplate")
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/application-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true)
    .WriteTo.Logger(databaseLogger => databaseLogger
        .Filter.ByIncludingOnly(IsDatabaseLogEvent)
        .WriteTo.File(
            "Logs/database-errors-.log",
            restrictedToMinimumLevel: LogEventLevel.Error,
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 90,
            shared: true))
    .CreateLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "EnterpriseTemplate")
        .ReadFrom.Services(services)
        .WriteTo.Console()
        .WriteTo.File(
            "Logs/application-.log",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30,
            shared: true)
        .WriteTo.Logger(databaseLogger => databaseLogger
            .Filter.ByIncludingOnly(IsDatabaseLogEvent)
            .WriteTo.File(
                "Logs/database-errors-.log",
                restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 90,
                shared: true)));

    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
    builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));
    builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("Cache"));
    builder.Services.Configure<AuthenticationProviderOptions>(builder.Configuration.GetSection("AuthenticationProvider"));
    builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));
    builder.Services.Configure<ActiveDirectoryOptions>(builder.Configuration.GetSection("ActiveDirectory"));

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPersistence(builder.Configuration);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IAuthenticationProvider, IdentityAuthenticationProvider>();
    builder.Services.AddScoped<IAuthenticationProvider, ActiveDirectoryAuthenticationProvider>();
    builder.Services.AddScoped<IAuthenticationProvider, EntraAuthenticationProvider>();
    builder.Services.AddScoped<EnterpriseTemplate.Application.Services.IAuthenticationService, ConfiguredAuthenticationService>();
    builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
    builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

    builder.Services.AddAuthentication()
        .AddNegotiate()
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

    builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add<AuditActionFilter>();
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });

    builder.Services.AddDataProtection()
        .SetApplicationName("EnterpriseTemplate");

    builder.Services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>();

    builder.Services.AddResponseCaching();
    builder.Services.AddOutputCache();
    builder.Services.AddRateLimiter(options =>
    {
        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            string key = context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

            return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 10
            });
        });
    });

    builder.Services.Configure<SecurityStampValidatorOptions>(options =>
    {
        options.ValidationInterval = TimeSpan.FromMinutes(30);
    });

    WebApplication app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms.";
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("CorrelationId", httpContext.Items["X-Correlation-Id"] ?? httpContext.TraceIdentifier);
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress?.ToString());
            diagnosticContext.Set("UserName", httpContext.User.Identity?.Name);
        };
    });
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseResponseCaching();
    app.UseOutputCache();
    app.UseRateLimiter();
    app.Use(async (context, next) =>
    {
        context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; frame-ancestors 'none'; base-uri 'self';";
        context.Response.Headers["X-Frame-Options"] = "DENY";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        await next().ConfigureAwait(false);
    });
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapHealthChecks("/health");
    app.MapControllerRoute("areas", "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
    app.MapControllerRoute("default", "{controller=Dashboard}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application terminated unexpectedly.");
}
finally
{
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}

static bool IsDatabaseLogEvent(LogEvent logEvent)
{
    if (logEvent.Properties.ContainsKey("DatabaseError"))
    {
        return true;
    }

    return logEvent.Properties.TryGetValue("SourceContext", out LogEventPropertyValue? sourceContext)
        && sourceContext.ToString().Contains("DatabaseExceptionLoggingInterceptor", StringComparison.Ordinal);
}
