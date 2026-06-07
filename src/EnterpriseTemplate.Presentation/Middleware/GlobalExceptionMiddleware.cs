using EnterpriseTemplate.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;

namespace EnterpriseTemplate.Presentation.Middleware;

/// <summary>
/// Converts exceptions into RFC 7807 problem details responses.
/// </summary>
public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
    /// </summary>
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Processes the HTTP request.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            if (IsDatabaseException(exception))
            {
                using (LogContext.PushProperty("DatabaseError", true))
                using (LogContext.PushProperty("CorrelationId", context.Items["X-Correlation-Id"] ?? context.TraceIdentifier))
                {
                    _logger.LogError(exception, "Database exception occurred while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
                }
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception occurred while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
            }

            await WriteProblemDetailsAsync(context, exception).ConfigureAwait(false);
        }
    }

    private static bool IsDatabaseException(Exception exception)
    {
        Exception? currentException = exception;

        while (currentException is not null)
        {
            if (currentException is DbUpdateException or DbUpdateConcurrencyException or SqlException or TimeoutException)
            {
                return true;
            }

            currentException = currentException.InnerException;
        }

        return false;
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
    {
        int statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            ValidationException => StatusCodes.Status400BadRequest,
            BusinessException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        ProblemDetails problem = new()
        {
            Status = statusCode,
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Instance = context.Request.Path
        };
        problem.Extensions["correlationId"] = context.Items["X-Correlation-Id"];

        if (exception is ValidationException validationException)
        {
            problem.Extensions["errors"] = validationException.Errors;
        }

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problem).ConfigureAwait(false);
    }
}
