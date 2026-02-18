public class Job
{
    public int Id { get; set; }

    // معلومات الشركة (Navigation Property)
    public string CompanyId { get; set; }
    public ApplicationUser Company { get; set; } = default!;
    public string CompanyName { get; set; }

    // تفاصيل الوظيفة
    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string JobType { get; set; }
    public List<string> JobRequirements { get; set; } = [];

    // معلومات إضافية
    public string? Location { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }

    // تواريخ
    public DateTime PostedDate { get; set; } = DateTime.Now;
    public DateTime? DeadlineDate { get; set; }

    // Status
    public bool IsActive { get; set; } = true;
}
public enum ExperienceLevel
    {
    EntryLevel,
    MidLevel,
    SeniorLevel,
    Manager,
    Director,
    Executive
}