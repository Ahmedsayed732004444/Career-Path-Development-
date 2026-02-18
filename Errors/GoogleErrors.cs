namespace Career_Path.Errors
{
    public record class GoogleErrors
    {
        public static Error RemoteError(string errorMessage) =>
            new("Google.RemoteError", $"Google authentication error: {errorMessage}", StatusCodes.Status400BadRequest);

        public static readonly Error LoginInfoNotFound =
            new("Google.LoginInfoNotFound", "Failed to load Google login information", StatusCodes.Status400BadRequest);

        public static readonly Error UserNotFoundAfterLogin =
            new("Google.UserNotFoundAfterLogin", "User not found after successful Google login", StatusCodes.Status400BadRequest);

        public static readonly Error EmailNotProvided =
            new("Google.EmailNotProvided", "Email not provided by Google", StatusCodes.Status400BadRequest);

        public static Error LinkAccountFailedWithDetails(IEnumerable<string> errors) =>
            new("Google.LinkAccountFailed", $"Failed to link Google account: {string.Join(", ", errors)}", StatusCodes.Status400BadRequest);

        public static Error UserCreationFailedWithDetails(IEnumerable<string> errors) =>
            new("Google.UserCreationFailed", $"Failed to create user: {string.Join(", ", errors)}", StatusCodes.Status400BadRequest);
    }
}
