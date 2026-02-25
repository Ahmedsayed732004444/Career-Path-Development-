namespace Career_Path.Contracts.Users
{
    public record MembershipUpgradeResponse
    (
        string Id                  ,
        string UserId              ,
        RequestStatus Status,
        string? Note,              
        DateTime RequestedAt
    );
}