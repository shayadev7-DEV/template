using System.ComponentModel.DataAnnotations;

namespace ProjectName.Presentation.ViewModels;

/// <summary>User view model displayed in MVC views.</summary>
public sealed record UserViewModel(
    Guid Id,
    string FullName,
    string Email,
    Status Status);

/// <summary>Create-user MVC form view model.</summary>
public sealed class CreateUserViewModel
{
    [Required]
    [MaxLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Display(Name = "Mobile number")]
    public string MobileNumber { get; init; } = string.Empty;

    [Display(Name = "User type")]
    public UserType UserType { get; init; } = UserType.Internal;

    public Gender Gender { get; init; } = Gender.Unknown;
}

/// <summary>Edit-user MVC form view model.</summary>
public sealed class EditUserViewModel
{
    public Guid Id { get; init; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; init; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Display(Name = "Mobile number")]
    public string MobileNumber { get; init; } = string.Empty;

    public Gender Gender { get; init; } = Gender.Unknown;
}

/// <summary>Login MVC form view model.</summary>
public sealed class LoginViewModel
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    public bool RememberMe { get; init; }
}
