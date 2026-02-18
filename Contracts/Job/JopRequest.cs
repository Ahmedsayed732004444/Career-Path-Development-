namespace Career_Path.Contracts.Job
{
    public record JopRequest
    (
         string JobTitle ,
         string JobType ,
         string JobDescription        ,
         string? Location              ,
         List<string> JobRequirements ,
         ExperienceLevel? ExperienceLevel ,
         decimal? SalaryMin           ,
         decimal? SalaryMax           ,
         DateTime? DeadlineDate       
      );
}
