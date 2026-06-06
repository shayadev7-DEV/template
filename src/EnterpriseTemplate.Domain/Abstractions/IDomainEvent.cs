namespace EnterpriseTemplate.Domain.Abstractions;

/// <summary>
/// Represents a business event raised by an aggregate.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the UTC date when the event occurred.
    /// </summary>
    DateTimeOffset OccurredOnUtc { get; }
}
