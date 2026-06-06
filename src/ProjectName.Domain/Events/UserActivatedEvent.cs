namespace ProjectName.Domain.Events;

/// <summary>Domain event raised after a user is activated.</summary>
public sealed record UserActivatedEvent(Guid UserId) : IDomainEvent
{
    public DateTimeOffset OccurredOnUtc { get; init; } = DateTimeOffset.UtcNow;
}
