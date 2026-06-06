namespace ProjectName.Presentation.Areas.Admin.Controllers;

/// <summary>Admin dashboard controller.</summary>
[Area("Admin")]
[Authorize(Roles = ApplicationConstants.AdminRole)]
public sealed class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
