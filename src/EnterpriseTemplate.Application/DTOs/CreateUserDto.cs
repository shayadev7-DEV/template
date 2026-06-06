using EnterpriseTemplate.Domain.Enums;

namespace EnterpriseTemplate.Application.DTOs;

/// <summary>
/// Data transfer object used to create users.
/// </summary>
public sealed class CreateUserDto
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional mobile number.
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// Gets or sets the user type.
    /// </summary>
    public UserType UserType { get; set; } = UserType.Local;
}
