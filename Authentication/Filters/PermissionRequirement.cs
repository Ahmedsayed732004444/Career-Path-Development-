namespace Career_Path.Authentication.Filters;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}