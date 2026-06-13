using spx.Domain.Abstractions;

namespace spx.Domain.Common;

/// <summary>
/// Base aggregate root for consistency boundaries.
/// </summary>
public abstract class AggregateRoot : AuditableEntity, IAggregateRoot
{
}
