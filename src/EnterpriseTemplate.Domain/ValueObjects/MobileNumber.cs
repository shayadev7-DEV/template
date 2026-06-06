using System.Text.RegularExpressions;

namespace EnterpriseTemplate.Domain.ValueObjects;

/// <summary>
/// Immutable mobile number value object stored in normalized international form when provided.
/// </summary>
public sealed class MobileNumber : IEquatable<MobileNumber>
{
    private const int MaxLength = 20;
    private static readonly Regex Pattern = new("^\\+?[0-9]{7,20}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private MobileNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the normalized mobile number.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a mobile number value object.
    /// </summary>
    public static MobileNumber Create(string value)
    {
        var normalized = value.Replace(" ", string.Empty, StringComparison.Ordinal).Replace("-", string.Empty, StringComparison.Ordinal);

        if (string.IsNullOrWhiteSpace(normalized) || normalized.Length > MaxLength || !Pattern.IsMatch(normalized))
        {
            throw new ArgumentException("Mobile number is invalid.", nameof(value));
        }

        return new MobileNumber(normalized);
    }

    /// <inheritdoc />
    public bool Equals(MobileNumber? other)
    {
        return other is not null && Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as MobileNumber);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode(StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }
}
