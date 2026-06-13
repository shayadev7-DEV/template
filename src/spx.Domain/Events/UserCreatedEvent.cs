using spx.Domain.Abstractions;

namespace spx.Domain.Events;

/// <summary>
/// Event raised when a user is created.
/// </summary>
public sealed class UserCreatedEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserCreatedEvent"/> class.
    /// </summary>
    public UserCreatedEvent(Guid userId)
    {
        UserId = userId;
        OccurredOnUtc = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public Guid UserId { get; }

    /// <inheritdoc />
    public DateTimeOffset OccurredOnUtc { get; }
}
