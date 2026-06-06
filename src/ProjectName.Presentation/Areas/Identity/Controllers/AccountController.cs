namespace ProjectName.Presentation.Areas.Identity.Controllers;

/// <summary>Identity account controller supporting configured hybrid login provider.</summary>
[Area("Identity")]
[AllowAnonymous]
public sealed class AccountController(IAuthenticationService authentication) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await authentication.LoginAsync(
            new LoginDto(model.UserName, model.Password, model.RememberMe),
            cancellationToken);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.FailureReason ?? "Login failed.");

            return View(model);
        }

        return RedirectToAction("Index", "Dashboard", new { area = string.Empty });
    }
}
