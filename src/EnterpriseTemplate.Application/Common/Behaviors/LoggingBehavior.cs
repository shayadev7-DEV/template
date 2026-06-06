using EnterpriseTemplate.Application.Abstractions;
using MediatR;

namespace EnterpriseTemplate.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that logs request execution without binding to a logging product.
/// </summary>
public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IApplicationLogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    public LoggingBehavior(IApplicationLogger logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.Information("Handling request {RequestName}.", typeof(TRequest).Name);
        TResponse response = await next().ConfigureAwait(false);
        _logger.Information("Handled request {RequestName}.", typeof(TRequest).Name);

        return response;
    }
}
