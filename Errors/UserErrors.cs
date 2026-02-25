namespace Career_Path.Errors;

public record UserErrors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

    public static readonly Error DisabledUser =
        new("User.DisabledUser", "Disabled user, please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error LockedUser =
        new("User.LockedUser", "Locked user, please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error UserNotFound =
    new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidRoles =
        new("Role.InvalidRoles", "Invalid roles", StatusCodes.Status400BadRequest);
    public static readonly Error DuplicateProfile =
        new("User.DuplicateProfile", "User profile already exists", StatusCodes.Status409Conflict);
    public static readonly Error ProfileNotFound =
        new("User.ProfileNotFound", "User profile not found", StatusCodes.Status404NotFound);
    public static readonly Error FileNotFound =
        new("User.FileNotFound", "File not found", StatusCodes.Status404NotFound);
    
    public static readonly Error DuplicatedRequest 
        = new("User.DuplicatedRequest", "A similar request is already in progress, please wait", StatusCodes.Status429TooManyRequests);
    public static readonly Error RequestNotFound =
        new("User.RequestNotFound", "Request not found", StatusCodes.Status404NotFound);
    public static readonly Error CannotRejectRequest =
        new("User.CannotRejectRequest", "Cannot reject a  request", StatusCodes.Status400BadRequest);
    public static readonly Error CannotApproveRequest =
        new("User.CannotApproveRequest", "Cannot approve a request", StatusCodes.Status400BadRequest);

}