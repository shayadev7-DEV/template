using FluentValidation;
using MediatR;
using ValidationException = EnterpriseTemplate.Application.Exceptions.ValidationException;

namespace EnterpriseTemplate.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that executes FluentValidation validators before handlers.
/// </summary>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        ValidationContext<TRequest> context = new(request);
        var failures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);
        var errors = failures
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .GroupBy(error => error.PropertyName)
            .ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray());

        if (errors.Count > 0)
        {
            throw new ValidationException("Validation failed.", errors);
        }

        return await next().ConfigureAwait(false);
    }
}
