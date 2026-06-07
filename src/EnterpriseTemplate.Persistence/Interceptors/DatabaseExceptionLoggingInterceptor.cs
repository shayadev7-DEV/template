using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EnterpriseTemplate.Persistence.Interceptors;

/// <summary>
/// Logs failed Entity Framework database commands so database-side errors are captured in a dedicated log file.
/// </summary>
public sealed class DatabaseExceptionLoggingInterceptor : DbCommandInterceptor
{
    private readonly ILogger<DatabaseExceptionLoggingInterceptor> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseExceptionLoggingInterceptor"/> class.
    /// </summary>
    public DatabaseExceptionLoggingInterceptor(ILogger<DatabaseExceptionLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {
        LogCommandFailure(command, eventData);
        base.CommandFailed(command, eventData);
    }

    /// <inheritdoc />
    public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        LogCommandFailure(command, eventData);
        return base.CommandFailedAsync(command, eventData, cancellationToken);
    }

    private void LogCommandFailure(DbCommand command, CommandErrorEventData eventData)
    {
        _logger.LogError(
            eventData.Exception,
            "Database command failed after {ElapsedMilliseconds} ms. CommandType: {CommandType}. CommandText: {CommandText}",
            eventData.Duration.TotalMilliseconds,
            command.CommandType,
            command.CommandText);
    }
}
