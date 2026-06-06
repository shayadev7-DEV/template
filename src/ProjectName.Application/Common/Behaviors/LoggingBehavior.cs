namespace ProjectName.Application.Common.Behaviors;

/// <summary>MediatR pipeline behavior that logs use-case execution through the application logging abstraction.</summary>
public sealed class LoggingBehavior<TRequest, TResponse>(
    IApplicationLogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.Information("Handling {RequestName}", typeof(TRequest).Name);
        var response = await next(cancellationToken);
        logger.Information("Handled {RequestName}", typeof(TRequest).Name);

        return response;
    }
}
