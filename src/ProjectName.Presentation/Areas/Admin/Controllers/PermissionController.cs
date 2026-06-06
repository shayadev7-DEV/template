namespace ProjectName.Presentation.Areas.Admin.Controllers;
/// <summary>Admin permission management controller.</summary>
[Area("Admin")]
[Authorize(Roles = ApplicationConstants.AdminRole)]
public sealed class PermissionController : Controller { public IActionResult Index() => View(); }
