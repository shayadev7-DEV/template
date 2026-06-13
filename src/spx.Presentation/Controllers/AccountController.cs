using spx.Application.DTOs;
using spx.Application.Services;
using spx.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spx.Presentation.Controllers;

/// <summary>
/// Handles interactive account authentication routes used by the application cookie.
/// </summary>
public sealed class AccountController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Shows the login form.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToLocal(returnUrl);
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    /// <summary>
    /// Attempts to sign in with the configured authentication provider.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        model.ReturnUrl = returnUrl ?? model.ReturnUrl;

        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            ModelState.AddModelError(nameof(model.UserName), "Username is required.");
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
            ModelState.AddModelError(nameof(model.Password), "Password is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var login = new LoginDto
        {
            UserName = model.UserName.Trim(),
            Password = model.Password,
            RememberMe = model.RememberMe
        };

        bool authenticated = await _authenticationService.LoginAsync(login, cancellationToken).ConfigureAwait(false);
        if (authenticated)
        {
            return RedirectToLocal(model.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    /// <summary>
    /// Signs the current user out.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await _authenticationService.LogoutAsync(cancellationToken).ConfigureAwait(false);
        return RedirectToAction(nameof(Login));
    }

    /// <summary>
    /// Shows an access denied page for authenticated users without permission.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction("Index", "Dashboard");
    }
}
