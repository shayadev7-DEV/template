namespace ProjectName.Presentation.Middleware;

/// <summary>Ensures every request has a correlation id for tracing and logs.</summary>
public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers.TryGetValue(ApplicationConstants.CorrelationIdHeader, out var value)
            ? value.ToString()
            : Guid.NewGuid().ToString("N");

        context.TraceIdentifier = correlationId;
        context.Response.Headers.TryAdd(ApplicationConstants.CorrelationIdHeader, correlationId);

        await next(context);
    }
}
