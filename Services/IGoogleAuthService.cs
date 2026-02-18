using Microsoft.AspNetCore.Authentication;

namespace Career_Path.Services
{
    public interface IGoogleAuthService
    {
        AuthenticationProperties ConfigureGoogleLogin(string returnUrl);
        Task<Result<AuthResponse>> HandleGoogleCallbackAsync(string remoteError, CancellationToken cancellationToken = default);
    }
}
