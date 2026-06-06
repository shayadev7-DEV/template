using Microsoft.AspNetCore.Authorization;

namespace EnterpriseTemplate.Presentation.Authorization;

/// <summary>
/// Authorization requirement for a dynamic permission code.
/// </summary>
public sealed class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
    /// </summary>
    public PermissionRequirement(string permissionCode)
    {
        PermissionCode = permissionCode;
    }

    /// <summary>
    /// Gets the permission code.
    /// </summary>
    public string PermissionCode { get; }
}
