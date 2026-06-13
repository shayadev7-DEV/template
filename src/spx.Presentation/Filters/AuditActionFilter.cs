using Microsoft.AspNetCore.Mvc.Filters;

namespace spx.Presentation.Filters;

/// <summary>
/// Captures action audit metadata for future audit sinks.
/// </summary>
public sealed class AuditActionFilter : IAsyncActionFilter
{
    private readonly ILogger<AuditActionFilter> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditActionFilter"/> class.
    /// </summary>
    public AuditActionFilter(ILogger<AuditActionFilter> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("Executing action {ActionName}.", context.ActionDescriptor.DisplayName);
        await next().ConfigureAwait(false);
    }
}
