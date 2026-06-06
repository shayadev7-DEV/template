namespace ProjectName.Presentation.Areas.Reports.Controllers;

/// <summary>Reports area dashboard controller.</summary>
[Area("Reports")]
[Authorize]
public sealed class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
