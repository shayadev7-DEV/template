namespace ProjectName.Domain.Events;

/// <summary>Domain event raised after a user is created.</summary>
public sealed record UserCreatedEvent(Guid UserId, string Email) : IDomainEvent
{
    public DateTimeOffset OccurredOnUtc { get; init; } = DateTimeOffset.UtcNow;
}
