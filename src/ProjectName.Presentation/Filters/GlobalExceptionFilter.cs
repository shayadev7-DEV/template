using Microsoft.AspNetCore.Mvc.Filters;
namespace ProjectName.Presentation.Filters;
/// <summary>MVC exception filter for view requests; APIs are handled by middleware.</summary>
public sealed class GlobalExceptionFilter : IExceptionFilter { public void OnException(ExceptionContext context) { } }
