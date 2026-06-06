using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ProjectName.Shared.Constants;

namespace ProjectName.Infrastructure.Authorization;
/// <summary>Authorization requirement representing one named permission.</summary>
public sealed class PermissionRequirement(string permission) : IAuthorizationRequirement { public string Permission { get; } = permission; }
/// <summary>Authorization handler that validates permission claims or service-backed permissions.</summary>
public sealed class PermissionHandler(IPermissionService permissions) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is not null && await permissions.UserHasPermissionAsync(userId, requirement.Permission)) context.Succeed(requirement);
    }
}
/// <summary>Dynamic policy provider that creates permission policies from names such as Permission:Users.Read.</summary>
public sealed class DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var prefix = ApplicationConstants.PermissionPolicyPrefix + ":";
        if (!policyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return await base.GetPolicyAsync(policyName);
        var permission = policyName[prefix.Length..];
        return new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(permission)).Build();
    }
}
