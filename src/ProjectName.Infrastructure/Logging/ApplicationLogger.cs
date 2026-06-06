namespace ProjectName.Infrastructure.Logging;
/// <summary>Adapter from the application logger abstraction to Microsoft.Extensions.Logging.</summary>
/// <remarks>Future Serilog/NLog/OpenTelemetry/etc. adapters can replace this without touching business logic.</remarks>
public sealed class ApplicationLogger<T>(ILogger<T> logger) : IApplicationLogger<T>
{
    public void Information(string messageTemplate, params object[] args) => logger.LogInformation(messageTemplate, args);
    public void Warning(string messageTemplate, params object[] args) => logger.LogWarning(messageTemplate, args);
    public void Error(Exception exception, string messageTemplate, params object[] args) => logger.LogError(exception, messageTemplate, args);
}
