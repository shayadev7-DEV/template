using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectName.Presentation.Filters;
using ProjectName.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(CacheOptions.SectionName));
builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.SectionName));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.SectionName));
builder.Services.Configure<ActiveDirectoryOptions>(builder.Configuration.GetSection(ActiveDirectoryOptions.SectionName));

builder.Services.AddApplication().AddInfrastructure().AddPersistence(builder.Configuration);
builder.Services.AddControllersWithViews(options => { options.Filters.Add<GlobalExceptionFilter>(); options.Filters.Add<AuditActionFilter>(); options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); });
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.Cookie.Name = "__Host-ProjectName"; options.Cookie.HttpOnly = true; options.Cookie.SecurePolicy = CookieSecurePolicy.Always; options.Cookie.SameSite = SameSiteMode.Strict; options.SlidingExpiration = true; });
builder.Services.AddAuthorization(options => { options.AddPolicy("AdminOnly", p => p.RequireRole(ApplicationConstants.AdminRole)); options.AddPolicy("CanReadUsers", p => p.Requirements.Add(new ProjectName.Infrastructure.Authorization.PermissionRequirement("Users.Read"))); });
builder.Services.AddDataProtection();
builder.Services.AddHealthChecks().AddDbContextCheck<ProjectName.Persistence.ApplicationDbContext>();
builder.Services.AddResponseCaching();
builder.Services.AddOutputCache();
builder.Services.AddRateLimiter(options => options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx => RateLimitPartition.GetFixedWindowLimiter(ctx.Connection.RemoteIpAddress?.ToString() ?? "anonymous", _ => new FixedWindowRateLimiterOptions { PermitLimit = 100, Window = TimeSpan.FromMinutes(1), QueueLimit = 0 })));
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (!app.Environment.IsDevelopment()) app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();
app.UseResponseCaching();
app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllerRoute("areas", "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Dashboard}/{action=Index}/{id?}");
app.Run();

/// <summary>Partial Program type for integration tests.</summary>
public partial class Program;
