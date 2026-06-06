using EnterpriseTemplate.Application.DTOs;
using EnterpriseTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseTemplate.Presentation.Authentication;

/// <summary>
/// Authentication provider backed by ASP.NET Core Identity.
/// </summary>
public sealed class IdentityAuthenticationProvider : IAuthenticationProvider
{
    private readonly SignInManager<IdentityApplicationUser> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityAuthenticationProvider"/> class.
    /// </summary>
    public IdentityAuthenticationProvider(SignInManager<IdentityApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    /// <inheritdoc />
    public string Name => "Identity";

    /// <inheritdoc />
    public async Task<bool> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, true).ConfigureAwait(false);

        return result.Succeeded;
    }
}
