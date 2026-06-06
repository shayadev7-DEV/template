namespace ProjectName.Application.Abstractions.Logging;
/// <summary>Application logging abstraction decoupled from Serilog, NLog, ELK, Seq, Application Insights, OpenTelemetry, Datadog, or Splunk.</summary>
/// <remarks>Business logic uses this port; adapters can be swapped in Infrastructure without changing use cases.</remarks>
public interface IApplicationLogger<T>
{
    void Information(string messageTemplate, params object[] args);
    void Warning(string messageTemplate, params object[] args);
    void Error(Exception exception, string messageTemplate, params object[] args);
}
