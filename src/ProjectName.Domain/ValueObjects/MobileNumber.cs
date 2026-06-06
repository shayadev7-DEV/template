using System.Text.RegularExpressions;

namespace ProjectName.Domain.ValueObjects;

/// <summary>Immutable international mobile number value object.</summary>
/// <remarks>Encapsulates normalization and validation of mobile numbers.</remarks>
public sealed record MobileNumber
{
    private static readonly Regex Pattern = new("^\\+?[1-9]\\d{7,14}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private MobileNumber(string value) => Value = value;
    public string Value { get; }
    public static MobileNumber Create(string value)
    {
        var normalized = (value ?? string.Empty).Trim().Replace(" ", string.Empty, StringComparison.Ordinal);
        if (!Pattern.IsMatch(normalized)) throw new DomainRuleException("Mobile number format is invalid.");
        return new MobileNumber(normalized);
    }
    public override string ToString() => Value;
}
