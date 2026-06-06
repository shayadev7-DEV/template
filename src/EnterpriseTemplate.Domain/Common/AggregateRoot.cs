using EnterpriseTemplate.Domain.Abstractions;

namespace EnterpriseTemplate.Domain.Common;

/// <summary>
/// Base aggregate root for consistency boundaries.
/// </summary>
public abstract class AggregateRoot : AuditableEntity, IAggregateRoot
{
}
