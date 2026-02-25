namespace Career_Path.Contracts.Job
{
    public record ApplyJobRequest
    (
         IFormFile CV,
         string Phone ,
         string? Notes
    );
}
