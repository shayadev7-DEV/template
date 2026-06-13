using spx.Domain.Common;
using spx.Domain.Enums;

namespace spx.Domain.Authorization;

/// <summary>
/// Permission granted to roles and resolved dynamically as policies.
/// </summary>
public sealed class Permission : AuditableEntity
{
    private Permission()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Permission"/> class.
    /// </summary>
    public Permission(string name, string code, PermissionType type)
    {
        Name = name;
        Code = code;
        Type = type;
    }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the unique permission code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets the permission type.
    /// </summary>
    public PermissionType Type { get; private set; }
}
