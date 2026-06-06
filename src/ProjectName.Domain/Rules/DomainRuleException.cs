namespace ProjectName.Domain.Rules;

/// <summary>Exception thrown when a domain invariant is violated.</summary>
/// <remarks>It is intentionally dependency-free so business rules stay inside Domain.</remarks>
public sealed class DomainRuleException(string message) : Exception(message);
