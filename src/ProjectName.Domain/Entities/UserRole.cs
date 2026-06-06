namespace ProjectName.Domain.Entities;

/// <summary>Join entity mapping application users to domain roles.</summary>
/// <remarks>Separate from Identity user roles so business permissions stay provider-agnostic.</remarks>
public sealed class UserRole : AuditableEntity
{
    private UserRole()
    {
    }

    private UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }

    public Guid RoleId { get; private set; }

    public static UserRole Create(Guid userId, Guid roleId)
    {
        return new UserRole(userId, roleId);
    }
}
