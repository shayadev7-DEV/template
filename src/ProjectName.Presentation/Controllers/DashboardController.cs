namespace ProjectName.Presentation.Controllers;

/// <summary>Main dashboard controller.</summary>
public sealed class DashboardController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
