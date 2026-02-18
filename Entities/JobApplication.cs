namespace Intelligent_Career_Advisor.Models;

public class JobApplication
{
    public int Id { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public string ApplicationSource { get; set; }  // For example: LinkedIn, Company Website, Referral
    public string? Notes { get; set; }
    public string? AttachmentUrl { get; set; } // For resume, cover letter, etc.

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
