namespace ProjectName.Domain.Entities;

/// <summary>User aggregate root containing business profile and status rules.</summary>
/// <remarks>Authentication provider data is mapped externally; domain rules protect user state transitions.</remarks>
public sealed class ApplicationUser : AggregateRoot
{
    private readonly List<UserRole> _roles = [];

    private ApplicationUser()
    {
        FullName = FullName.Create("System", "User");
        Email = Email.Create("system@example.com");
        MobileNumber = MobileNumber.Create("+10000000000");
    }

    private ApplicationUser(
        FullName fullName,
        Email email,
        MobileNumber mobileNumber,
        UserType userType,
        Gender gender)
    {
        FullName = fullName;
        Email = email;
        MobileNumber = mobileNumber;
        UserType = userType;
        Gender = gender;
        Status = Status.Active;

        RaiseDomainEvent(new UserCreatedEvent(Id, Email.Value));
    }

    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public MobileNumber MobileNumber { get; private set; }

    public UserType UserType { get; private set; }

    public Gender Gender { get; private set; }

    public Status Status { get; private set; }

    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public static ApplicationUser Create(
        FullName fullName,
        Email email,
        MobileNumber mobileNumber,
        UserType userType,
        Gender gender)
    {
        return new ApplicationUser(fullName, email, mobileNumber, userType, gender);
    }

    public void UpdateProfile(FullName fullName, MobileNumber mobileNumber, Gender gender)
    {
        FullName = fullName;
        MobileNumber = mobileNumber;
        Gender = gender;
    }

    public void Activate()
    {
        if (Status == Status.Active)
        {
            return;
        }

        Status = Status.Active;
        RaiseDomainEvent(new UserActivatedEvent(Id));
    }

    public void Deactivate()
    {
        if (Status == Status.Inactive)
        {
            return;
        }

        Status = Status.Inactive;
        RaiseDomainEvent(new UserDeactivatedEvent(Id));
    }

    public void AssignRole(Guid roleId)
    {
        if (_roles.Any(x => x.RoleId == roleId))
        {
            return;
        }

        _roles.Add(UserRole.Create(Id, roleId));
    }
}
