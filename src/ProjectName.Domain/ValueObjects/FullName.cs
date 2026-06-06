namespace ProjectName.Domain.ValueObjects;

/// <summary>Immutable full name value object.</summary>
/// <remarks>Prevents partially empty person names from entering the domain model.</remarks>
public sealed record FullName
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string DisplayName => $"{FirstName} {LastName}";

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainRuleException("First name is required.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainRuleException("Last name is required.");
        }

        return new FullName(firstName.Trim(), lastName.Trim());
    }

    public override string ToString()
    {
        return DisplayName;
    }
}
