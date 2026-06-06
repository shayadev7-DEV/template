namespace ProjectName.Presentation.Middleware;

/// <summary>Adds baseline security headers for OWASP-aligned browser hardening.</summary>
public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
        context.Response.Headers.TryAdd("X-XSS-Protection", "0");
        context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
        context.Response.Headers.TryAdd("Permissions-Policy", "camera=(), microphone=(), geolocation=(), payment=()");
        context.Response.Headers.TryAdd("Cross-Origin-Opener-Policy", "same-origin");
        context.Response.Headers.TryAdd("Cross-Origin-Resource-Policy", "same-origin");
        context.Response.Headers.TryAdd(
            "Content-Security-Policy",
            "default-src 'self'; base-uri 'self'; object-src 'none'; frame-ancestors 'none'; " +
            "img-src 'self' data:; style-src 'self' 'unsafe-inline'; script-src 'self'; form-action 'self'");

        await next(context);
    }
}
