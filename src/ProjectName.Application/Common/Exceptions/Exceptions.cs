namespace ProjectName.Application.Common.Exceptions;
/// <summary>Exception raised when a requested resource cannot be found.</summary>
public sealed class NotFoundException(string message) : Exception(message);
/// <summary>Exception raised for use-case validation failures.</summary>
public sealed class ValidationException(IDictionary<string, string[]> errors) : Exception("Validation failed.") { public IDictionary<string, string[]> Errors { get; } = errors; }
/// <summary>Exception raised when business rules reject an operation.</summary>
public sealed class BusinessException(string message) : Exception(message);
/// <summary>Exception raised when a principal lacks required authorization.</summary>
public sealed class ForbiddenException(string message = "Forbidden.") : Exception(message);
/// <summary>Exception raised when authentication is required or invalid.</summary>
public sealed class UnauthorizedException(string message = "Unauthorized.") : Exception(message);
