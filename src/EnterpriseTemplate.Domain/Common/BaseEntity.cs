using EnterpriseTemplate.Domain.Abstractions;

namespace EnterpriseTemplate.Domain.Common;

/// <summary>
/// Base entity that stores identity and domain events.
/// </summary>
public abstract class BaseEntity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    /// <inheritdoc />
    public Guid Id { get; protected set; }

    /// <summary>
    /// Gets domain events raised by the entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to be dispatched after persistence succeeds.
    /// </summary>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clears domain events after dispatching.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
