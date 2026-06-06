namespace EnterpriseTemplate.Application.DTOs;

/// <summary>
/// Data transfer object used to update users.
/// </summary>
public sealed class UpdateUserDto
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional mobile number.
    /// </summary>
    public string? MobileNumber { get; set; }
}
