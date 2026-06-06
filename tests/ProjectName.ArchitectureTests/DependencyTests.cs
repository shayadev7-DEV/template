using Xunit;
using NetArchTest.Rules;
using ProjectName.Domain.Common;

namespace ProjectName.ArchitectureTests;
/// <summary>Architecture tests enforce Clean Architecture dependency rules.</summary>
public sealed class DependencyTests
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Other_ProjectName_Projects()
    {
        var result = Types.InAssembly(typeof(BaseEntity).Assembly).ShouldNot().HaveDependencyOnAny("ProjectName.Application", "ProjectName.Infrastructure", "ProjectName.Persistence", "ProjectName.Presentation", "ProjectName.Shared").GetResult();
        Assert.True(result.IsSuccessful);
    }
}
