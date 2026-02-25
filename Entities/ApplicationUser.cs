namespace Career_Path.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7().ToString();
        SecurityStamp = Guid.CreateVersion7().ToString();
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public  UserProfile? UserProfile { get; set; }
    public  ModelExtration ModelExtration { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public List<Job> PostedJobs { get; set; } = [];
    public List<JobSubmission> JobSubmissions { get; set; } = [];
    public List<PrompetRoadMap> PrompetRoadMaps { get; set; } = [];
}