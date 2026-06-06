namespace ProjectName.Application.Abstractions.Services;

/// <summary>User use-case facade for MVC controllers and APIs.</summary>
public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default);

    Task<UserDto> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken = default);

    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<UserListDto>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
