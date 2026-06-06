using System.Text.RegularExpressions;

namespace EnterpriseTemplate.Domain.ValueObjects;

/// <summary>
/// Immutable email value object with domain validation.
/// </summary>
public sealed class Email : IEquatable<Email>
{
    private const int MaxLength = 320;
    private static readonly Regex Pattern = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the normalized email value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a valid email value object.
    /// </summary>
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MaxLength || !Pattern.IsMatch(value))
        {
            throw new ArgumentException("Email address is invalid.", nameof(value));
        }

        return new Email(value.Trim().ToLowerInvariant());
    }

    /// <inheritdoc />
    public bool Equals(Email? other)
    {
        return other is not null && Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Email);
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
