namespace ProjectName.Application.Abstractions.Persistence;
/// <summary>Generic repository abstraction for aggregate persistence operations.</summary>
/// <remarks>Defined in Application so business use cases depend on abstractions rather than EF Core implementations.</remarks>
public interface IGenericRepository<TEntity> where TEntity : class, IAggregateRoot
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> Query();
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
