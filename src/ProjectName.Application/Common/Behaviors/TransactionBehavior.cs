namespace ProjectName.Application.Common.Behaviors;

/// <summary>MediatR pipeline behavior that wraps commands in a Unit of Work transaction.</summary>
public sealed class TransactionBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = default;

        await unitOfWork.ExecuteInTransactionAsync(
            async ct =>
            {
                response = await next(ct);
            },
            cancellationToken);

        return response!;
    }
}
