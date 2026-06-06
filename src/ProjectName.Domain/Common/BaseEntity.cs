namespace ProjectName.Domain.Common;

/// <summary>Base implementation for entities that need identity and domain event support.</summary>
/// <remarks>Placed in Domain because identity and event collection are core domain mechanics.</remarks>
public abstract class BaseEntity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>Initializes a new entity with a generated identifier.</summary>
    protected BaseEntity() => Id = Guid.NewGuid();

    /// <inheritdoc />
    public Guid Id { get; protected init; }

    /// <summary>Gets pending domain events raised by the entity.</summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>Adds a new domain event.</summary>
    /// <param name="domainEvent">Event describing a completed business fact.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    /// <summary>Clears events after dispatching.</summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}
