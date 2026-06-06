namespace ProjectName.Infrastructure.Persistence;

/// <summary>EF Core-backed generic repository implementation.</summary>
/// <remarks>Infrastructure owns data-access mechanics while Application depends only on IGenericRepository.</remarks>
public sealed class GenericRepository<TEntity>(
    IApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : class, IAggregateRoot
{
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _set.FindAsync([id], cancellationToken).AsTask();
    }

    public IQueryable<TEntity> Query()
    {
        return _set.AsQueryable();
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _set.AddAsync(entity, cancellationToken).AsTask();
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _set.Remove(entity);
    }
}
