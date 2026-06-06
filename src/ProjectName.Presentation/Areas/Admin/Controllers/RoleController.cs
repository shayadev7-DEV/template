namespace ProjectName.Presentation.Areas.Admin.Controllers;
/// <summary>Admin role management controller.</summary>
[Area("Admin")]
[Authorize(Policy = "Permission:Roles.Manage")]
public sealed class RoleController : Controller { public IActionResult Index() => View(); }
