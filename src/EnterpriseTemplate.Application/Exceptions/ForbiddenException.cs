namespace EnterpriseTemplate.Application.Exceptions;

/// <summary>
/// Exception raised when an authenticated user is forbidden from an operation.
/// </summary>
public sealed class ForbiddenException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    public ForbiddenException(string message) : base(message)
    {
    }
}
