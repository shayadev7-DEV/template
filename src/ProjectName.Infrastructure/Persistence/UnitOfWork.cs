namespace ProjectName.Infrastructure.Persistence;
/// <summary>Unit of Work implementation that commits EF Core changes and transactions.</summary>
public sealed class UnitOfWork(IApplicationDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        if (context is not DbContext dbContext) { await action(cancellationToken); await SaveChangesAsync(cancellationToken); return; }
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        await action(cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
