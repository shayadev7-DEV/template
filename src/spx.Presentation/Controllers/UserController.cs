using spx.Application.DTOs;
using spx.Application.Services;
using spx.Presentation.ViewModels;
using spx.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace spx.Presentation.Controllers;

/// <summary>
/// Thin MVC controller for user management.
/// </summary>
[Authorize]
public sealed class UserController : BaseController
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 20;
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Lists users.
    /// </summary>
    [Authorize(Policy = PolicyNames.UsersRead)]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var users = await _userService.GetPagedAsync(DefaultPageNumber, DefaultPageSize, cancellationToken).ConfigureAwait(false);

        return View(users);
    }

    /// <summary>
    /// Displays the create form.
    /// </summary>
    [Authorize(Policy = PolicyNames.UsersCreate)]
    public IActionResult Create()
    {
        return View(new CreateUserViewModel());
    }

    /// <summary>
    /// Creates a user.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = PolicyNames.UsersCreate)]
    public async Task<IActionResult> Create(CreateUserViewModel model, CancellationToken cancellationToken)
    {
        CreateUserDto dto = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
        await _userService.CreateAsync(dto, cancellationToken).ConfigureAwait(false);

        return RedirectToAction(nameof(Index));
    }
}
