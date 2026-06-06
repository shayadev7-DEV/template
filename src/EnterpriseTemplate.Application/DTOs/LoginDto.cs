namespace EnterpriseTemplate.Application.DTOs;

/// <summary>
/// Data transfer object used for login requests.
/// </summary>
public sealed class LoginDto
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether persistent cookie is requested.
    /// </summary>
    public bool RememberMe { get; set; }
}
