namespace ProjectName.Application.Services;

/// <summary>Application service implementing user CRUD use cases.</summary>
/// <remarks>Coordinates repositories, validation-ready DTOs, mapping, and Unit of Work without persistence details.</remarks>
public sealed class UserService(
    IGenericRepository<ApplicationUser> users,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserService
{
    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        var user = ApplicationUser.Create(
            FullName.Create(dto.FirstName, dto.LastName),
            Email.Create(dto.Email),
            MobileNumber.Create(dto.MobileNumber),
            dto.UserType,
            dto.Gender);

        await users.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        var user = await users.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"User '{id}' was not found.");

        user.UpdateProfile(
            FullName.Create(dto.FirstName, dto.LastName),
            MobileNumber.Create(dto.MobileNumber),
            dto.Gender);

        users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await users.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"User '{id}' was not found.");

        return mapper.Map<UserDto>(user);
    }

    public async Task<IReadOnlyCollection<UserListDto>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        const int maxPageSize = 100;

        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, maxPageSize);

        return await users
            .Query()
            .AsNoTracking()
            .OrderBy(x => x.FullName.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserListDto(x.Id, x.FullName.DisplayName, x.Email.Value, x.Status))
            .ToListAsync(cancellationToken);
    }
}
