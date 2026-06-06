using System.ComponentModel.DataAnnotations;

namespace EnterpriseTemplate.Presentation.ViewModels;

/// <summary>
/// View model used for local login.
/// </summary>
public sealed class LoginViewModel
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether persistent login is requested.
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    /// Gets or sets the local URL to redirect to after successful login.
    /// </summary>
    public string? ReturnUrl { get; set; }
}
