namespace EnterpriseTemplate.Application.Exceptions;

/// <summary>
/// Exception raised when a business rule is violated.
/// </summary>
public sealed class BusinessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class.
    /// </summary>
    public BusinessException(string message) : base(message)
    {
    }
}
