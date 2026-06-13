using spx.Domain.Abstractions;

namespace spx.Domain.Events;

/// <summary>
/// Event raised when a user is deactivated.
/// </summary>
public sealed class UserDeactivatedEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserDeactivatedEvent"/> class.
    /// </summary>
    public UserDeactivatedEvent(Guid userId)
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
