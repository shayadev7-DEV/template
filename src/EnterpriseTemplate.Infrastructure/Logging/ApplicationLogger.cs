using EnterpriseTemplate.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace EnterpriseTemplate.Infrastructure.Logging;

/// <summary>
/// Default adapter from the application logging abstraction to Microsoft.Extensions.Logging.
/// </summary>
public sealed class ApplicationLogger : IApplicationLogger
{
    private readonly ILogger<ApplicationLogger> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationLogger"/> class.
    /// </summary>
    public ApplicationLogger(ILogger<ApplicationLogger> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public void Information(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    /// <inheritdoc />
    public void Warning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    /// <inheritdoc />
    public void Error(Exception exception, string message, params object[] args)
    {
        _logger.LogError(exception, message, args);
    }
}
