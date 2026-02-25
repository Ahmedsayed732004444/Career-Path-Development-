namespace Career_Path.Contracts.Job
{
    public record ApplyJobResponse
    (
         string Id ,        
         string ApplicantionId  ,
         string CVPath          ,
         string Phone           ,
         string? Notes          ,
         DateTime AppliedAt     
    );
    
}
