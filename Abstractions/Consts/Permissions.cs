namespace Career_Path.Abstractions.Consts;

public static class Permissions
{
    public static string Type { get; } = "permissions";
   // Permitions
    public const string GetUsers = "users:read";
    public const string AddUsers = "users:add";
    public const string UpdateUsers = "users:update";

    public const string GetRoles = "roles:read";
    public const string AddRoles = "roles:add";
    public const string UpdateRoles = "roles:update";

    public const string GetProfile = "profile:read";
    public const string UpdateProfile = "profile:update";

    public const string GetJobs = "jobs:read";
    public const string AddJobs = "jobs:add";
    public const string UpdateJobs = "jobs:update";
    
    public const string GetJobApplicants = "jobApplicants:read";

    public const string GetMembershipUpgradeRequests = "membershipUpgradeRequests:read";
    public const string ApproveMembershipUpgradeRequests = "membershipUpgradeRequests:approve";
    public const string RejectMembershipUpgradeRequests = "membershipUpgradeRequests:reject";

    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
}