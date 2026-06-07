using EnterpriseTemplate.Domain.Common;
using EnterpriseTemplate.Domain.Enums;
using EnterpriseTemplate.Domain.Events;
using EnterpriseTemplate.Domain.ValueObjects;

namespace EnterpriseTemplate.Domain.Users;

/// <summary>
/// User aggregate root that encapsulates identity profile business rules.
/// </summary>
public sealed class ApplicationUser : AggregateRoot
{
    private ApplicationUser()
    {
        Email = Email.Create("placeholder@example.com");
        FullName = FullName.Create("Placeholder", "User");
    }

    private ApplicationUser(Email email, FullName fullName, UserType userType)
    {
        Email = email;
        FullName = fullName;
        UserType = userType;
        Status = Status.Active;
        AddDomainEvent(new UserCreatedEvent(Id));
    }

    /// <summary>
    /// Gets the email value object.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the mobile number value object.
    /// </summary>
    public MobileNumber? MobileNumber { get; private set; }

    /// <summary>
    /// Gets the full name value object.
    /// </summary>
    public FullName FullName { get; private set; }

    /// <summary>
    /// Gets the user type.
    /// </summary>
    public UserType UserType { get; private set; }

    /// <summary>
    /// Gets the user status.
    /// </summary>
    public Status Status { get; private set; }

    /// <summary>
    /// Creates a new user aggregate.
    /// </summary>
    public static ApplicationUser Create(Email email, FullName fullName, UserType userType)
    {
        return new ApplicationUser(email, fullName, userType);
    }

    /// <summary>
    /// Updates user profile information.
    /// </summary>
    public void UpdateProfile(FullName fullName, MobileNumber? mobileNumber)
    {
        FullName = fullName;
        MobileNumber = mobileNumber;
    }

    /// <summary>
    /// Activates the user when not already active.
    /// </summary>
    public void Activate()
    {
        if (Status == Status.Active)
        {
            return;
        }

        Status = Status.Active;
        AddDomainEvent(new UserActivatedEvent(Id));
    }

    /// <summary>
    /// Deactivates the user when active.
    /// </summary>
    public void Deactivate()
    {
        if (Status == Status.Inactive)
        {
            return;
        }

        Status = Status.Inactive;
        AddDomainEvent(new UserDeactivatedEvent(Id));
    }
}
