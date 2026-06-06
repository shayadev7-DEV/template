using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Areas.Identity.Controllers;

/// <summary>
/// Dashboard controller for the Identity area.
/// </summary>
[Area("Identity")]
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
