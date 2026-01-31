using Microsoft.AspNetCore.Authentication;

namespace Career_Path.Services
{
    public interface IGitHubAuthService
    {
        AuthenticationProperties ConfigureGitHubLogin(string returnUrl);
        Task<Result<AuthResponse>> HandleGitHubCallbackAsync(string remoteError, CancellationToken cancellationToken = default);
    }
}
