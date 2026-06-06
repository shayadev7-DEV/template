namespace ProjectName.Presentation.Areas.Admin.Controllers;
/// <summary>Admin user CRUD controller.</summary>
[Area("Admin")]
[Authorize(Policy = "Permission:Users.Read")]
public sealed class UserController(IUserService users) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 20, CancellationToken cancellationToken = default) => View(await users.GetPagedAsync(page, pageSize, cancellationToken));
    [HttpGet]
    [Authorize(Policy = "Permission:Users.Create")]
    public IActionResult Create() => View(new CreateUserViewModel());
    [HttpPost]
    [Authorize(Policy = "Permission:Users.Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);
        await users.CreateAsync(new CreateUserDto(model.FirstName, model.LastName, model.Email, model.MobileNumber, model.UserType, model.Gender), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
