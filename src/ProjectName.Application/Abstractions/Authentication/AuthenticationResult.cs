namespace ProjectName.Application.Abstractions.Authentication;

/// <summary>Authentication result returned by any configured provider.</summary>
public sealed record AuthenticationResult(bool Succeeded, string? UserName, string? FailureReason)
{
    public static AuthenticationResult Success(string userName)
    {
        return new AuthenticationResult(true, userName, null);
    }

    public static AuthenticationResult Failed(string reason)
    {
        return new AuthenticationResult(false, null, reason);
    }
}
