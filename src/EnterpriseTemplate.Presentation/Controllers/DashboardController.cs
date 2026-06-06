using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Controllers;

/// <summary>
/// Controller responsible for the application dashboard.
/// </summary>
[Authorize]
public sealed class DashboardController : BaseController
{
    /// <summary>
    /// Renders the dashboard index page.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}
