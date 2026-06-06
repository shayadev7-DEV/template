namespace ProjectName.Presentation.Areas.Settings.Controllers;
/// <summary>Settings area dashboard controller.</summary>
[Area("Settings")]
[Authorize]
public sealed class DashboardController : Controller { public IActionResult Index() => View(); }
