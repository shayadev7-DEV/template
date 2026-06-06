namespace ProjectName.Application.Abstractions.Services;

/// <summary>Role management use-case abstraction.</summary>
public interface IRoleService
{
    Task<Guid> CreateAsync(string name, CancellationToken cancellationToken = default);
}
