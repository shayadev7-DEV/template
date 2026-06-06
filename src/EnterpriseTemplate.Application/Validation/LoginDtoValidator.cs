using EnterpriseTemplate.Application.DTOs;
using FluentValidation;

namespace EnterpriseTemplate.Application.Validation;

/// <summary>
/// Validator for login requests.
/// </summary>
public sealed class LoginDtoValidator : AbstractValidator<LoginDto>
{
    private const int UserNameMaxLength = 256;
    private const int PasswordMaxLength = 256;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginDtoValidator"/> class.
    /// </summary>
    public LoginDtoValidator()
    {
        RuleFor(model => model.UserName).NotEmpty().MaximumLength(UserNameMaxLength);
        RuleFor(model => model.Password).NotEmpty().MaximumLength(PasswordMaxLength);
    }
}
