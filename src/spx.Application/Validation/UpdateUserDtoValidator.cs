using spx.Application.DTOs;
using FluentValidation;

namespace spx.Application.Validation;

/// <summary>
/// Validator for user update requests.
/// </summary>
public sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    private const int NameMaxLength = 100;
    private const int MobileMaxLength = 20;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserDtoValidator"/> class.
    /// </summary>
    public UpdateUserDtoValidator()
    {
        RuleFor(model => model.Id).NotEmpty();
        RuleFor(model => model.FirstName).NotEmpty().MaximumLength(NameMaxLength);
        RuleFor(model => model.LastName).NotEmpty().MaximumLength(NameMaxLength);
        RuleFor(model => model.MobileNumber).MaximumLength(MobileMaxLength).When(model => !string.IsNullOrWhiteSpace(model.MobileNumber));
    }
}
