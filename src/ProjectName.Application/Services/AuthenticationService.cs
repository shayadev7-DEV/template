namespace ProjectName.Application.Services;
/// <summary>Selects and executes the configured authentication provider strategy.</summary>
public sealed class AuthenticationService(IEnumerable<IAuthenticationProvider> providers, Microsoft.Extensions.Options.IOptions<AuthenticationOptions> options) : IAuthenticationService
{
    public Task<AuthenticationResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var provider = providers.FirstOrDefault(x => string.Equals(x.Name, options.Value.Provider, StringComparison.OrdinalIgnoreCase)) ?? throw new BusinessException($"Authentication provider '{options.Value.Provider}' is not registered.");
        return provider.AuthenticateAsync(dto, cancellationToken);
    }
}
