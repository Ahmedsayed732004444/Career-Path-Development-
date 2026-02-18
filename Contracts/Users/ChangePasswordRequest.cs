namespace Career_Path.Contracts.Users;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);