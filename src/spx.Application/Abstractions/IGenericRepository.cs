using System.Linq.Expressions;
using spx.Domain.Abstractions;

namespace spx.Application.Abstractions;

/// <summary>
/// Repository abstraction for aggregate persistence and query composition.
/// </summary>
public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets an entity by identifier.
    /// </summary>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a read-only page of entities.
    /// </summary>
    Task<IReadOnlyList<TEntity>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Finds entities matching a predicate.
    /// </summary>
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Adds an entity.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Removes an entity.
    /// </summary>
    void Remove(TEntity entity);
}
