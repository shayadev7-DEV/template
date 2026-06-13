namespace spx.Application.Exceptions;

/// <summary>
/// Exception raised when a request is not authenticated.
/// </summary>
public sealed class UnauthorizedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    public UnauthorizedException(string message) : base(message)
    {
    }
}
