using System.Text.RegularExpressions;

namespace ProjectName.Domain.ValueObjects;

/// <summary>Immutable normalized email value object.</summary>
/// <remarks>Validation lives in Domain because an invalid email is never valid business state.</remarks>
public sealed record Email
{
    private static readonly Regex Pattern = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private Email(string value) => Value = value;
    public string Value { get; }
    public static Email Create(string value)
    {
        var normalized = (value ?? string.Empty).Trim().ToLowerInvariant();
        if (!Pattern.IsMatch(normalized)) throw new DomainRuleException("Email format is invalid.");
        return new Email(normalized);
    }
    public override string ToString() => Value;
}
