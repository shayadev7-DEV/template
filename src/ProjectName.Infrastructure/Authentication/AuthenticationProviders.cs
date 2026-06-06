namespace ProjectName.Infrastructure.Authentication;

/// <summary>ASP.NET Core Identity authentication provider strategy.</summary>
public sealed class IdentityAuthenticationProvider : IAuthenticationProvider
{
    public string Name => "Identity";

    public Task<AuthenticationResult> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AuthenticationResult.Success(login.UserName));
    }
}

/// <summary>LDAP/Active Directory authentication provider strategy placeholder.</summary>
/// <remarks>Production implementations should bind through secure LDAP and map AD groups to roles.</remarks>
public sealed class ActiveDirectoryAuthenticationProvider(
    IOptions<ActiveDirectoryOptions> options) : IAuthenticationProvider
{
    public string Name => "ActiveDirectory";

    public Task<AuthenticationResult> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(options.Value.LdapUrl))
        {
            return Task.FromResult(AuthenticationResult.Failed("LDAP endpoint is not configured."));
        }

        return Task.FromResult(AuthenticationResult.Success(login.UserName));
    }
}

/// <summary>Microsoft Entra ID authentication provider strategy placeholder.</summary>
/// <remarks>Real token validation is configured in Presentation through OpenID Connect/JWT middleware.</remarks>
public sealed class EntraAuthenticationProvider : IAuthenticationProvider
{
    public string Name => "EntraId";

    public Task<AuthenticationResult> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AuthenticationResult.Success(login.UserName));
    }
}

/// <summary>Windows Authentication provider strategy placeholder.</summary>
public sealed class WindowsAuthenticationProvider : IAuthenticationProvider
{
    public string Name => "Windows";

    public Task<AuthenticationResult> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AuthenticationResult.Success(login.UserName));
    }
}
