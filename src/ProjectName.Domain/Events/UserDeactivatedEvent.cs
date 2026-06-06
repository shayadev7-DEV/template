namespace ProjectName.Domain.Events;

/// <summary>Domain event raised after a user is deactivated.</summary>
public sealed record UserDeactivatedEvent(Guid UserId) : IDomainEvent
{
    public DateTimeOffset OccurredOnUtc { get; init; } = DateTimeOffset.UtcNow;
}
