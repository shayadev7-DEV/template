using spx.Application.DTOs;

namespace spx.Application.Services;

/// <summary>
/// Service responsible for authentication workflows.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a login request using the configured provider.
    /// </summary>
    Task<bool> LoginAsync(LoginDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Signs out the current user.
    /// </summary>
    Task LogoutAsync(CancellationToken cancellationToken);
}
