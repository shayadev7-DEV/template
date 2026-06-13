using AutoMapper;
using spx.Application.DTOs;
using spx.Domain.Users;

namespace spx.Application.Mapping;

/// <summary>
/// AutoMapper profile for user mappings.
/// </summary>
public sealed class UserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfile"/> class.
    /// </summary>
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email.Value))
            .ForMember(destination => destination.DisplayName, options => options.MapFrom(source => source.FullName.DisplayName))
            .ForMember(destination => destination.MobileNumber, options => options.MapFrom(source => source.MobileNumber == null ? null : source.MobileNumber.Value));

        CreateMap<ApplicationUser, UserListDto>()
            .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email.Value))
            .ForMember(destination => destination.DisplayName, options => options.MapFrom(source => source.FullName.DisplayName));
    }
}
