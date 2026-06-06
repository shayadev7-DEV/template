using EnterpriseTemplate.Application.Abstractions;
using MediatR;

namespace EnterpriseTemplate.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior reserved for transactional commands.
/// </summary>
public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).Name.EndsWith("Command", StringComparison.Ordinal))
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async token => await next().ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
        }

        return await next().ConfigureAwait(false);
    }
}
