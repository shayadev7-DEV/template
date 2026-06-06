namespace ProjectName.Domain.Entities;

/// <summary>Join entity mapping roles to permissions.</summary>
/// <remarks>Maintained as explicit entity to support audit fields and future scoped permissions.</remarks>
public sealed class RolePermission : AuditableEntity
{
    private RolePermission()
    {
    }

    private RolePermission(Guid roleId, Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public Guid RoleId { get; private set; }

    public Guid PermissionId { get; private set; }

    public static RolePermission Create(Guid roleId, Guid permissionId)
    {
        return new RolePermission(roleId, permissionId);
    }
}
