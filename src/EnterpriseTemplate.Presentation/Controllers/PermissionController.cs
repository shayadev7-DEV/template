using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Controllers;

/// <summary>
/// Thin MVC controller for permission management.
/// </summary>
[Authorize(Roles = "Administrator")]
public sealed class PermissionController : BaseController
{
    /// <summary>
    /// Lists permissions.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}
