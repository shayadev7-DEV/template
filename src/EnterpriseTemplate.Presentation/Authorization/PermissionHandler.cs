using System.Security.Claims;
using EnterpriseTemplate.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace EnterpriseTemplate.Presentation.Authorization;

/// <summary>
/// Authorization handler that resolves permission grants through the application service.
/// </summary>
public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionHandler"/> class.
    /// </summary>
    public PermissionHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        string? userIdValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (Guid.TryParse(userIdValue, out Guid userId) && await _permissionService.UserHasPermissionAsync(userId, requirement.PermissionCode, CancellationToken.None).ConfigureAwait(false))
        {
            context.Succeed(requirement);
        }
    }
}
