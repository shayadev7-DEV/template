namespace ProjectName.Domain.Common;

/// <summary>Base type for aggregates that own invariants and consistency boundaries.</summary>
/// <remarks>Placed in Domain to prevent persistence concerns from leaking into aggregate design.</remarks>
public abstract class AggregateRoot : AuditableEntity, IAggregateRoot;
