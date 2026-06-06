using AutoMapper;
using EnterpriseTemplate.Application.Abstractions;
using EnterpriseTemplate.Application.DTOs;
using EnterpriseTemplate.Application.Exceptions;
using EnterpriseTemplate.Domain.Users;
using EnterpriseTemplate.Domain.ValueObjects;
using FluentValidation;

namespace EnterpriseTemplate.Application.Services;

/// <summary>
/// Service responsible for managing users and enforcing user business workflows.
/// </summary>
public sealed class UserService : IUserService
{
    private readonly IGenericRepository<ApplicationUser> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserDto> _createValidator;
    private readonly IValidator<UpdateUserDto> _updateValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    public UserService(
        IGenericRepository<ApplicationUser> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateUserDto> createValidator,
        IValidator<UpdateUserDto> updateValidator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <inheritdoc />
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (user is null)
        {
            throw new NotFoundException($"User '{id}' was not found.");
        }

        return _mapper.Map<UserDto>(user);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<UserListDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var users = await _repository.ListAsync(pageNumber, pageSize, cancellationToken).ConfigureAwait(false);

        return _mapper.Map<IReadOnlyList<UserListDto>>(users);
    }

    /// <inheritdoc />
    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
        await _createValidator.ValidateAndThrowAsync(dto, cancellationToken).ConfigureAwait(false);

        var user = ApplicationUser.Create(
            Email.Create(dto.Email),
            FullName.Create(dto.FirstName, dto.LastName),
            dto.UserType);

        if (!string.IsNullOrWhiteSpace(dto.MobileNumber))
        {
            user.UpdateProfile(user.FullName, MobileNumber.Create(dto.MobileNumber));
        }

        await _repository.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return _mapper.Map<UserDto>(user);
    }

    /// <inheritdoc />
    public async Task<UserDto> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        await _updateValidator.ValidateAndThrowAsync(dto, cancellationToken).ConfigureAwait(false);
        var user = await _repository.GetByIdAsync(dto.Id, cancellationToken).ConfigureAwait(false);

        if (user is null)
        {
            throw new NotFoundException($"User '{dto.Id}' was not found.");
        }

        MobileNumber? mobileNumber = string.IsNullOrWhiteSpace(dto.MobileNumber) ? null : MobileNumber.Create(dto.MobileNumber);
        user.UpdateProfile(FullName.Create(dto.FirstName, dto.LastName), mobileNumber);
        _repository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return _mapper.Map<UserDto>(user);
    }
}
