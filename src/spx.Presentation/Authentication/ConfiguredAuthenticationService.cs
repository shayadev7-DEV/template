using spx.Application.DTOs;
using spx.Application.Services;
using spx.Shared.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace spx.Presentation.Authentication;

/// <summary>
/// Application authentication service that delegates to the configured provider.
/// </summary>
public sealed class ConfiguredAuthenticationService : IAuthenticationService
{
    private readonly IEnumerable<IAuthenticationProvider> _providers;
    private readonly AuthenticationProviderOptions _options;
    private readonly SignInManager<Persistence.Identity.IdentityApplicationUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfiguredAuthenticationService"/> class.
    /// </summary>
    public ConfiguredAuthenticationService(
        IEnumerable<IAuthenticationProvider> providers,
        IOptions<AuthenticationProviderOptions> options,
        SignInManager<Persistence.Identity.IdentityApplicationUser> signInManager)
    {
        _providers = providers;
        _options = options.Value;
        _signInManager = signInManager;
    }

    /// <inheritdoc />
    public Task<bool> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        IAuthenticationProvider provider = _providers.First(item => string.Equals(item.Name, _options.ActiveProvider, StringComparison.OrdinalIgnoreCase));

        return provider.AuthenticateAsync(dto, cancellationToken);
    }

    /// <inheritdoc />
    public Task LogoutAsync(CancellationToken cancellationToken)
    {
        return _signInManager.SignOutAsync();
    }
}
