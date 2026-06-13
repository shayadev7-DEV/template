using spx.Application.DTOs;

namespace spx.Application.Services;

/// <summary>
/// Service responsible for user use cases.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a user by identifier.
    /// </summary>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a paged user list.
    /// </summary>
    Task<IReadOnlyList<UserListDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a user.
    /// </summary>
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a user.
    /// </summary>
    Task<UserDto> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken);
}
