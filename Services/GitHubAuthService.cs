using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Career_Path.Services;

public class GitHubAuthService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider,
    ILogger<GitHubAuthService> logger,
    ApplicationDbContext context,
    IConfiguration configuration) : IGitHubAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ILogger<GitHubAuthService> _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;
    private readonly int _refreshTokenExpiryDays = 14;

    public AuthenticationProperties ConfigureGitHubLogin(string returnUrl)
    {
        // استخدام الرابط من الإعدادات بدلاً من كتابته مباشرة
        var redirectUrl = _configuration["Authentication:GitHub:CallbackUrl"]?? "http://localhost:5250/api/authentication/github/callback";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("GitHub", redirectUrl);

        // التحقق من صحة returnUrl قبل حفظه
        if (!string.IsNullOrEmpty(returnUrl) && IsValidReturnUrl(returnUrl))
        {
            properties.Items["returnUrl"] = returnUrl;
        }
        return properties;
    }
    // دالة جديدة للتحقق من صحة returnUrl
    private bool IsValidReturnUrl(string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
            return false;

        var allowedUrls = _configuration.GetSection("Authentication:GitHub:AllowedReturnUrls")
            .Get<string[]>() ?? Array.Empty<string>();

        if (!Uri.TryCreate(returnUrl, UriKind.RelativeOrAbsolute, out var uri))
            return false;

        // السماح بالروابط النسبية
        if (!uri.IsAbsoluteUri)
            return true;

        // التحقق من القائمة البيضاء للروابط المطلقة
        return allowedUrls.Any(allowed =>uri.ToString().StartsWith(allowed, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Result<AuthResponse>> HandleGitHubCallbackAsync(string remoteError,CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(remoteError))
        {
            _logger.LogError("GitHub authentication error: {Error}", remoteError);
            return Result.Failure<AuthResponse>(GitHubErrors.RemoteError(remoteError));
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            _logger.LogError("Failed to load external login information from GitHub");
            return Result.Failure<AuthResponse>(GitHubErrors.LoginInfoNotFound);
        }

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,info.ProviderKey,isPersistent: false,bypassTwoFactor: true);

        ApplicationUser? user = null;

        if (signInResult.Succeeded)
        {
            user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user is null)
            {
                _logger.LogError("User not found after successful login");
                return Result.Failure<AuthResponse>(GitHubErrors.UserNotFoundAfterLogin);
            }
        }
        else
        {
            var createUserResult = await CreateOrLinkGitHubUserAsync(info, cancellationToken);

            if (createUserResult.IsFailure)
                return Result.Failure<AuthResponse>(createUserResult.Error);

            user = createUserResult.Value;
        }

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(UserErrors.LockedUser);

        var (userRoles, userPermissions) = await GetUserRolesAndPermissionsAsync(user, cancellationToken);
        var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);
        var response = new AuthResponse(user.Id,user.Email,user.FirstName,user.LastName,token,expiresIn,refreshToken,refreshTokenExpiration);

        return Result.Success(response);
    }

    
    private async Task<Result<ApplicationUser>> CreateOrLinkGitHubUserAsync(ExternalLoginInfo info,CancellationToken cancellationToken)
    {
        var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(userEmail))
        {
            _logger.LogError("Email not provided by GitHub");
            return Result.Failure<ApplicationUser>(GitHubErrors.EmailNotProvided);
        }

        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            var createResult = await CreateNewGitHubUserAsync(userEmail, info, cancellationToken);

            if (createResult.IsFailure)
                return createResult;

            user = createResult.Value;
        }

        var addLoginResult = await _userManager.AddLoginAsync(user, info);

        if (!addLoginResult.Succeeded)
        {
            var errors = addLoginResult.Errors.Select(e => e.Description);
            _logger.LogError("Failed to link GitHub account for user {Email}", userEmail);

            return Result.Failure<ApplicationUser>(GitHubErrors.LinkAccountFailedWithDetails(errors));
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return Result.Success(user);
    }

    private async Task<Result<ApplicationUser>> CreateNewGitHubUserAsync(string email,ExternalLoginInfo info,CancellationToken cancellationToken)
    {
        var userName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? email.Split('@')[0];

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = userName,
            LastName = string.Empty
        };

        var createResult = await _userManager.CreateAsync(user);

        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors.Select(e => e.Description);
            _logger.LogError("Failed to create user {Email}", email);
            return Result.Failure<ApplicationUser>(GitHubErrors.UserCreationFailedWithDetails(errors));
        }

        await _userManager.AddToRoleAsync(user, DefaultRoles.Member.Name);

        var userProfile = new UserProfile
        {
            ApplicationUserId = user.Id
        };

        _context.UserProfiles.Add(userProfile);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(user);
    }

    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissionsAsync(
        ApplicationUser user,
        CancellationToken cancellationToken)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var userPermissions = await (from r in _context.Roles
                                     join p in _context.RoleClaims
                                     on r.Id equals p.RoleId
                                     where userRoles.Contains(r.Name!)
                                     select p.ClaimValue!)
                                     .Distinct()
                                     .ToListAsync(cancellationToken);

        return (userRoles, userPermissions);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}