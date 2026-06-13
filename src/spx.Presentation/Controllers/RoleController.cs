using spx.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spx.Presentation.Controllers;

/// <summary>
/// Thin MVC controller for Identity role containers.
/// </summary>
[Authorize(Policy = PolicyNames.PermissionsManage)]
public sealed class RoleController : BaseController
{
    /// <summary>
    /// Lists Identity roles that act as permission containers.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }
}
