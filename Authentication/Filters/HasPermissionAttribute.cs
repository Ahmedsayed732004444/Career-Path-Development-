namespace Career_Path.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}