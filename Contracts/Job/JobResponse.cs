using Microsoft.Identity.Client;

namespace Career_Path.Contracts.Job
{
    public record JobResponse
     (
         string Id,
         string JobTitle,
         string JobType,
         string JobDescription,
         string? Location,
         List<string> JobRequirements,
         ExperienceLevel? ExperienceLevel,
         decimal? SalaryMin,
         decimal? SalaryMax,
         DateTime PostedDate,
         DateTime? DeadlineDate,
         bool IsActive
      );
} 