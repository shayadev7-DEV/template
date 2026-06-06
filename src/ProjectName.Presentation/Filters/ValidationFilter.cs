using Microsoft.AspNetCore.Mvc.Filters;
namespace ProjectName.Presentation.Filters;
/// <summary>Short-circuits invalid MVC model state requests.</summary>
public sealed class ValidationFilter : IActionFilter { public void OnActionExecuting(ActionExecutingContext context) { if (!context.ModelState.IsValid) context.Result = new BadRequestObjectResult(context.ModelState); } public void OnActionExecuted(ActionExecutedContext context) { } }
