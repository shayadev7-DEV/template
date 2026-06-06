namespace ProjectName.Presentation.Controllers;
/// <summary>Base MVC controller with common helpers for all presentation controllers.</summary>
[Authorize]
public abstract class BaseController : Controller
{
    protected IActionResult RedirectToDefault() => RedirectToAction("Index", "Dashboard");
}
