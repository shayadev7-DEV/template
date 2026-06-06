namespace ProjectName.Application.Abstractions.Services;

/// <summary>Coordinates configured authentication providers.</summary>
public interface IAuthenticationService
{
    Task<AuthenticationResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
}
