namespace ProjectName.Application.Common.Behaviors;
/// <summary>MediatR pipeline behavior that executes FluentValidation validators before handlers.</summary>
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = validators.Select(v => v.Validate(request)).SelectMany(r => r.Errors).Where(f => f is not null).GroupBy(f => f.PropertyName).ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());
        if (failures.Count > 0) throw new ValidationException(failures);
        return await next(cancellationToken);
    }
}
