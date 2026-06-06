using EnterpriseTemplate.Application.DTOs;
using FluentValidation;

namespace EnterpriseTemplate.Application.Validation;

/// <summary>
/// Validator for user creation requests.
/// </summary>
public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    private const int NameMaxLength = 100;
    private const int EmailMaxLength = 320;
    private const int MobileMaxLength = 20;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserDtoValidator"/> class.
    /// </summary>
    public CreateUserDtoValidator()
    {
        RuleFor(model => model.FirstName).NotEmpty().MaximumLength(NameMaxLength);
        RuleFor(model => model.LastName).NotEmpty().MaximumLength(NameMaxLength);
        RuleFor(model => model.Email).NotEmpty().EmailAddress().MaximumLength(EmailMaxLength);
        RuleFor(model => model.MobileNumber).MaximumLength(MobileMaxLength).When(model => !string.IsNullOrWhiteSpace(model.MobileNumber));
        RuleFor(model => model.UserType).IsInEnum();
    }
}
