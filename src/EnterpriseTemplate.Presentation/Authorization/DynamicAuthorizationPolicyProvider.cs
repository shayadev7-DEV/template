using EnterpriseTemplate.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace EnterpriseTemplate.Presentation.Authorization;

/// <summary>
/// Dynamically creates authorization policies from permission policy names.
/// </summary>
public sealed class DynamicAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicAuthorizationPolicyProvider"/> class.
    /// </summary>
    public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    /// <inheritdoc />
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName).ConfigureAwait(false);

        if (policy is not null)
        {
            return policy;
        }

        string prefix = $"{PolicyNames.PermissionPrefix}:";

        if (policyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            string permission = policyName[prefix.Length..];

            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new PermissionRequirement(permission)).Build();
        }

        return null;
    }
}
