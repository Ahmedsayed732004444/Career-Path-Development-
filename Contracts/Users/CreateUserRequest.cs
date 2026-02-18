namespace Career_Path.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);