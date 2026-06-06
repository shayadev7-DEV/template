namespace EnterpriseTemplate.Application.Exceptions;

/// <summary>
/// Exception raised when application validation fails.
/// </summary>
public sealed class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets validation errors grouped by property name.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
