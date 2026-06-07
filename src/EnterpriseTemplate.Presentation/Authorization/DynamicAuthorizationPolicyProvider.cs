using EnterpriseTemplate.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace EnterpriseTemplate.Presentation.Authorization;

/// <summary>
/// Dynamically creates authorization policies from permission code policy names.
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

        string permissionCode = GetPermissionCode(policyName);

        if (IsPermissionCode(permissionCode))
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirement(permissionCode))
                .Build();
        }

        return null;
    }

    private static string GetPermissionCode(string policyName)
    {
        string prefix = $"{PolicyNames.PermissionPrefix}:";

        return policyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            ? policyName[prefix.Length..]
            : policyName;
    }

    private static bool IsPermissionCode(string policyName)
    {
        return !string.IsNullOrWhiteSpace(policyName)
            && policyName.Contains('.', StringComparison.Ordinal)
            && policyName.All(character => char.IsLetterOrDigit(character) || character is '.' or '_' or '-');
    }
}
