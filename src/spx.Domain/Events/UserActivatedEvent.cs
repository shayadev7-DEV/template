using spx.Domain.Abstractions;

namespace spx.Domain.Events;

/// <summary>
/// Event raised when a user is activated.
/// </summary>
public sealed class UserActivatedEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserActivatedEvent"/> class.
    /// </summary>
    public UserActivatedEvent(Guid userId)
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
