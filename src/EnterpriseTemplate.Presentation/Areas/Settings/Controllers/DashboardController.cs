using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Areas.Settings.Controllers;

/// <summary>
/// Dashboard controller for the Settings area.
/// </summary>
[Area("Settings")]
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
