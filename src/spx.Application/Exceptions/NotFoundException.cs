namespace spx.Application.Exceptions;

/// <summary>
/// Exception raised when a requested resource cannot be found.
/// </summary>
public sealed class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException(string message) : base(message)
    {
    }
}
