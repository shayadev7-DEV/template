using Microsoft.AspNetCore.Mvc.Filters;
namespace ProjectName.Presentation.Filters;
/// <summary>Action filter hook for audit trails of MVC actions.</summary>
public sealed class AuditActionFilter : IActionFilter { public void OnActionExecuting(ActionExecutingContext context) { } public void OnActionExecuted(ActionExecutedContext context) { } }
