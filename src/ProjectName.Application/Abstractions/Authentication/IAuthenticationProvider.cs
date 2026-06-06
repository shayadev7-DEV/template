namespace ProjectName.Application.Abstractions.Authentication;
/// <summary>Provider strategy contract for hybrid authentication.</summary>
/// <remarks>Implementations can wrap ASP.NET Core Identity, LDAP, Windows auth, or Entra ID without changing business logic.</remarks>
public interface IAuthenticationProvider { string Name { get; } Task<AuthenticationResult> AuthenticateAsync(LoginDto login, CancellationToken cancellationToken = default); }
