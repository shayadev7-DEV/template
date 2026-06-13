namespace spx.Shared.Constants;

/// <summary>
/// Authorization policy and permission constants.
/// </summary>
public static class PolicyNames
{
    /// <summary>
    /// Legacy prefix accepted by the dynamic policy provider for backward compatibility.
    /// </summary>
    public const string PermissionPrefix = "Permission";

    /// <summary>
    /// Permission required to view users.
    /// </summary>
    public const string UsersRead = "users.read";

    /// <summary>
    /// Permission required to create users.
    /// </summary>
    public const string UsersCreate = "users.create";

    /// <summary>
    /// Permission required to update users.
    /// </summary>
    public const string UsersUpdate = "users.update";

    /// <summary>
    /// Permission required to delete users.
    /// </summary>
    public const string UsersDelete = "users.delete";

    /// <summary>
    /// Permission required to manage permission assignments.
    /// </summary>
    public const string PermissionsManage = "permissions.manage";
}
