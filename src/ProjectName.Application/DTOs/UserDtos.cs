namespace ProjectName.Application.DTOs;
/// <summary>Input model for creating a user.</summary>
public sealed record CreateUserDto(string FirstName, string LastName, string Email, string MobileNumber, UserType UserType, Gender Gender);
/// <summary>Input model for updating a user profile.</summary>
public sealed record UpdateUserDto(string FirstName, string LastName, string MobileNumber, Gender Gender);
/// <summary>Detailed user output DTO.</summary>
public sealed record UserDto(Guid Id, string FullName, string Email, string MobileNumber, UserType UserType, Gender Gender, Status Status);
/// <summary>Lightweight list projection for pagination.</summary>
public sealed record UserListDto(Guid Id, string FullName, string Email, Status Status);
/// <summary>Login input used by authentication providers.</summary>
public sealed record LoginDto(string UserName, string Password, bool RememberMe);
