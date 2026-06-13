using spx.Application.DTOs;
using Microsoft.Extensions.Options;
using spx.Shared.Options;

namespace spx.Presentation.Authentication;

/// <summary>
/// Authentication provider placeholder for LDAP/Active Directory validation.
/// </summary>
public sealed class ActiveDirectoryAuthenticationProvider : IAuthenticationProvider
{
    private readonly ActiveDirectoryOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActiveDirectoryAuthenticationProvider"/> class.
    /// </summary>
    public ActiveDirectoryAuthenticationProvider(IOptions<ActiveDirectoryOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    public string Name => "ActiveDirectory";

    /// <inheritdoc />
    public Task<bool> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken)
    {
        bool configured = !string.IsNullOrWhiteSpace(_options.Domain) || !string.IsNullOrWhiteSpace(_options.LdapPath);

        return Task.FromResult(configured && !string.IsNullOrWhiteSpace(login.UserName));
    }
}
