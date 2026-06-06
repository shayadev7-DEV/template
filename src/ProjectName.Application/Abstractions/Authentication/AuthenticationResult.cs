namespace ProjectName.Application.Abstractions.Authentication;
/// <summary>Authentication result returned by any configured provider.</summary>
public sealed record AuthenticationResult(bool Succeeded, string? UserName, string? FailureReason)
{
    public static AuthenticationResult Success(string userName) => new(true, userName, null);
    public static AuthenticationResult Failed(string reason) => new(false, null, reason);
}
