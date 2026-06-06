namespace ProjectName.Application.Mapping;
/// <summary>AutoMapper profile for user projections.</summary>
/// <remarks>Mapping belongs to Application because DTOs are use-case contracts.</remarks>
public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForCtorParam(nameof(UserDto.FullName), o => o.MapFrom(s => s.FullName.DisplayName))
            .ForCtorParam(nameof(UserDto.Email), o => o.MapFrom(s => s.Email.Value))
            .ForCtorParam(nameof(UserDto.MobileNumber), o => o.MapFrom(s => s.MobileNumber.Value));
        CreateMap<ApplicationUser, UserListDto>()
            .ForCtorParam(nameof(UserListDto.FullName), o => o.MapFrom(s => s.FullName.DisplayName))
            .ForCtorParam(nameof(UserListDto.Email), o => o.MapFrom(s => s.Email.Value));
    }
}
