using Microsoft.AspNetCore.Mvc.Filters;

namespace EnterpriseTemplate.Presentation.Filters;

/// <summary>
/// MVC exception filter kept for view-specific extension points; API errors are handled by middleware.
/// </summary>
public sealed class GlobalExceptionFilter : IAsyncExceptionFilter
{
    /// <inheritdoc />
    public Task OnExceptionAsync(ExceptionContext context)
    {
        return Task.CompletedTask;
    }
}
