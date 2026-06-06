using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplate.Presentation.Controllers;

/// <summary>
/// Thin MVC controller for role management.
/// </summary>
[Authorize(Roles = "Administrator")]
public sealed class RoleController : BaseController
{
    /// <summary>
    /// Lists roles.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}
