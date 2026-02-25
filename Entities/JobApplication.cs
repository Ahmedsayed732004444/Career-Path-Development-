namespace Intelligent_Career_Advisor.Models;

public class JobApplication
{
    public string Id { get; set; } = Guid.CreateVersion7().ToString();
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public string ApplicationSource { get; set; }  // For example: LinkedIn, Company Website, Referral
    public string? Notes { get; set; }

    // Foreign key to ApplicationUser
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

}
public enum ApplicationStatus
{
    Applied,
    Interviewed,
    Offered,
    Rejected,
    Accepted
}
