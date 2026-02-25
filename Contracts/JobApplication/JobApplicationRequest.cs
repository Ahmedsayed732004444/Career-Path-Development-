using Intelligent_Career_Advisor.Models;

namespace Career_Path.Contracts.JobApplication;

public record JobApplicationRequest
(
    string JobTitle,
    string CompanyName,
    DateTime ApplicationDate,
    ApplicationStatus Status,
    string ApplicationSource,
    string? Notes
);
