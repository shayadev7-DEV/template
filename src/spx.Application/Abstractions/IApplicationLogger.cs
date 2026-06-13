namespace spx.Application.Abstractions;

/// <summary>
/// Application logging abstraction that avoids hard dependencies on concrete logging products.
/// </summary>
public interface IApplicationLogger
{
    /// <summary>
    /// Writes an informational log entry.
    /// </summary>
    void Information(string message, params object[] args);

    /// <summary>
    /// Writes a warning log entry.
    /// </summary>
    void Warning(string message, params object[] args);

    /// <summary>
    /// Writes an error log entry.
    /// </summary>
    void Error(Exception exception, string message, params object[] args);
}
