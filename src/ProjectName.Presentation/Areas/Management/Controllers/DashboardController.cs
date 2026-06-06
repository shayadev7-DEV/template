namespace ProjectName.Presentation.Areas.Management.Controllers;
/// <summary>Management area dashboard controller.</summary>
[Area("Management")]
[Authorize]
public sealed class DashboardController : Controller { public IActionResult Index() => View(); }
