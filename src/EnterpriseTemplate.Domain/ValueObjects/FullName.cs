namespace EnterpriseTemplate.Domain.ValueObjects;

/// <summary>
/// Immutable full-name value object.
/// </summary>
public sealed class FullName : IEquatable<FullName>
{
    private const int MaxPartLength = 100;

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string DisplayName => $"{FirstName} {LastName}";

    /// <summary>
    /// Creates a valid full name value object.
    /// </summary>
    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MaxPartLength)
        {
            throw new ArgumentException("First name is invalid.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MaxPartLength)
        {
            throw new ArgumentException("Last name is invalid.", nameof(lastName));
        }

        return new FullName(firstName.Trim(), lastName.Trim());
    }

    /// <inheritdoc />
    public bool Equals(FullName? other)
    {
        return other is not null && FirstName == other.FirstName && LastName == other.LastName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as FullName);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }
}
