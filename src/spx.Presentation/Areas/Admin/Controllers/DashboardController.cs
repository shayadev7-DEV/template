using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spx.Presentation.Areas.Admin.Controllers;

/// <summary>
/// Dashboard controller for the Admin area.
/// </summary>
[Area("Admin")]
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
