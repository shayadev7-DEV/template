namespace ProjectName.Domain.Abstractions;

/// <summary>Represents a domain object with a stable identity.</summary>
/// <remarks>Kept in Domain to preserve dependency-free entity semantics for Clean Architecture.</remarks>
public interface IEntity
{
    /// <summary>Gets the primary identifier.</summary>
    Guid Id { get; }
}
