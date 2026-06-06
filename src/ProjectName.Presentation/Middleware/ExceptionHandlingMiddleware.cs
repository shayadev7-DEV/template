using Microsoft.AspNetCore.Mvc;
using ProjectName.Application.Abstractions.Logging;

namespace ProjectName.Presentation.Middleware;
/// <summary>Converts exceptions to RFC7807 ProblemDetails responses with correlation id support.</summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, IApplicationLogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (Exception ex) { await HandleAsync(context, ex); }
    }
    private async Task HandleAsync(HttpContext context, Exception exception)
    {
        var status = exception switch { NotFoundException => StatusCodes.Status404NotFound, ValidationException => StatusCodes.Status400BadRequest, ForbiddenException => StatusCodes.Status403Forbidden, UnauthorizedException => StatusCodes.Status401Unauthorized, _ => StatusCodes.Status500InternalServerError };
        logger.Error(exception, "Unhandled exception {CorrelationId}", context.TraceIdentifier);
        var problem = new ProblemDetails { Status = status, Title = exception.GetType().Name, Detail = status == 500 ? "An unexpected error occurred." : exception.Message, Instance = context.Request.Path };
        problem.Extensions["correlationId"] = context.TraceIdentifier;
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
