namespace EnterpriseTemplate.Domain.Abstractions;

/// <summary>
/// Defines the minimum contract for a domain entity with a stable identity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    Guid Id { get; }
}
