namespace ProjectName.Domain.Abstractions;

/// <summary>Represents an immutable business event raised by a domain aggregate.</summary>
/// <remarks>Events are collected by aggregates and dispatched by application/infrastructure pipelines.</remarks>
public interface IDomainEvent
{
    /// <summary>Gets the UTC instant at which the event occurred.</summary>
    DateTimeOffset OccurredOnUtc { get; }
}
