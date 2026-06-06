namespace ProjectName.Domain.Abstractions;

/// <summary>Marker interface for aggregate roots that enforce transactional consistency boundaries.</summary>
/// <remarks>Repositories should be exposed only for aggregate roots.</remarks>
public interface IAggregateRoot : IEntity;
