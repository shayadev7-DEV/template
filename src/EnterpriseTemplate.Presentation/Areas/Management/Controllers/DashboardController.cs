using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Areas.Management.Controllers;

/// <summary>
/// Dashboard controller for the Management area.
/// </summary>
[Area("Management")]
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
