using spx.Application.DTOs;

namespace spx.Presentation.Authentication;

/// <summary>
/// Abstraction for hybrid authentication providers.
/// </summary>
public interface IAuthenticationProvider
{
    /// <summary>
    /// Gets the provider name used by configuration.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Authenticates a login request.
    /// </summary>
    Task<bool> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken);
}
