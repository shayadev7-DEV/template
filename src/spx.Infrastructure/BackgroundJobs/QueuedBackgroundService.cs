using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace spx.Infrastructure.BackgroundJobs;

/// <summary>
/// Background service placeholder for queued operational jobs.
/// </summary>
public sealed class QueuedBackgroundService : BackgroundService
{
    private static readonly TimeSpan Delay = TimeSpan.FromSeconds(30);
    private readonly ILogger<QueuedBackgroundService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueuedBackgroundService"/> class.
    /// </summary>
    public QueuedBackgroundService(ILogger<QueuedBackgroundService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogDebug("Background service heartbeat.");
            await Task.Delay(Delay, stoppingToken).ConfigureAwait(false);
        }
    }
}
