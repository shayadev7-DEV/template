using Xunit;
using ProjectName.Domain.ValueObjects;

namespace ProjectName.UnitTests;
/// <summary>Unit tests for domain value objects.</summary>
public sealed class EmailTests
{
    [Fact]
    public void Create_Normalizes_Email() => Assert.Equal("user@example.com", Email.Create(" USER@EXAMPLE.COM ").Value);
}
