using spx.Application.DTOs;

namespace spx.Presentation.Authentication;

/// <summary>
/// Authentication provider marker for Azure Entra ID interactive flows.
/// </summary>
public sealed class EntraAuthenticationProvider : IAuthenticationProvider
{
    /// <inheritdoc />
    public string Name => "Entra";

    /// <inheritdoc />
    public Task<bool> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken)
    {
        return Task.FromResult(false);
    }
}
