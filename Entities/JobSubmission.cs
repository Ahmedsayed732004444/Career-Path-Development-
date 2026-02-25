namespace Career_Path.Entities
{
    public class JobSubmission
    {
        public string Id { get; set; } = Guid.CreateVersion7().ToString();

        // المتقدم
        public string ApplicantId { get; set; } 
        public ApplicationUser ApplicationUser { get; set; } 

        // الوظيفة
        public string JobId { get; set; } = default!;
        public Job Job { get; set; } = default!;

        // بيانات التقديم
        public string CVPath { get; set; } 
        public string Phone { get; set; } 
        public string? Notes { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
