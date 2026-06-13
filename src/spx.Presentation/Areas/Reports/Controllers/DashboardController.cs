using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spx.Presentation.Areas.Reports.Controllers;

/// <summary>
/// Dashboard controller for the Reports area.
/// </summary>
[Area("Reports")]
[Authorize]
public sealed class DashboardController : Controller
{
    /// <summary>
    /// Renders the area dashboard.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}
