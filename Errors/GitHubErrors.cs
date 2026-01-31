namespace Career_Path.Errors;

public static class GitHubErrors
{
    public static readonly Error AuthenticationError = new(
        "GitHub.AuthenticationError",
        "An error occurred during GitHub authentication",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error LoginInfoNotFound = new(
        "GitHub.LoginInfoNotFound",
        "Unable to load external login information from GitHub",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error EmailNotProvided = new(
        "GitHub.EmailNotProvided",
        "Email not provided by GitHub",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error UserCreationFailed = new(
        "GitHub.UserCreationFailed",
        "Failed to create user account",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error LinkAccountFailed = new(
        "GitHub.LinkAccountFailed",
        "Failed to link GitHub account to user",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error UserNotFoundAfterLogin = new(
        "GitHub.UserNotFoundAfterLogin",
        "User not found after successful login",
        StatusCodes.Status500InternalServerError
    );

    public static readonly Error AccessDenied = new(
        "GitHub.AccessDenied",
        "Access denied by GitHub",
        StatusCodes.Status403Forbidden
    );

    public static readonly Error InvalidState = new(
        "GitHub.InvalidState",
        "Invalid state parameter received from GitHub",
        StatusCodes.Status400BadRequest
    );

    public static Error RemoteError(string errorMessage) => new(
        "GitHub.RemoteError",
        $"Error from GitHub: {errorMessage}",
        StatusCodes.Status400BadRequest
    );

    public static Error UserCreationFailedWithDetails(IEnumerable<string> errors) => new(
        "GitHub.UserCreationFailed",
        $"Failed to create user: {string.Join(", ", errors)}",
        StatusCodes.Status400BadRequest
    );

    public static Error LinkAccountFailedWithDetails(IEnumerable<string> errors) => new(
        "GitHub.LinkAccountFailed",
        $"Failed to link GitHub account: {string.Join(", ", errors)}",
        StatusCodes.Status400BadRequest
    );
}