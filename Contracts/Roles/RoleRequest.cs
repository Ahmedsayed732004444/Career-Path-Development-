namespace Career_Path.Contracts.Roles;

public record RoleRequest(
    string Name,
    IList<string> Permissions
);