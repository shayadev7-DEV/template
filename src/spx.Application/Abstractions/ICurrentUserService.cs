namespace spx.Application.Abstractions;

/// <summary>
/// Provides current request user context for auditing and authorization decisions.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user identifier when authenticated.
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the current user display name when authenticated.
    /// </summary>
    string? UserName { get; }
}
