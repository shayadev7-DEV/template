namespace spx.Application.Abstractions;

/// <summary>
/// Coordinates transactional persistence across repositories.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves pending changes in a single transaction.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Executes a function inside a database transaction.
    /// </summary>
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken);
}
