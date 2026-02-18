namespace Career_Path.Contracts.JopSearch
{

    public record JobSearchResponse
     (
         int Count,
         double Mean,
         List<JobDto> Results
     );

    public record JobDto
    (
        string Id,
        string Title,
        string Description,
        string Created,
        string RedirectUrl,
        string? Adref,
        double Latitude,
        double Longitude,
        LocationDto Location,
        CategoryDto? Category,
        CompanyDto? Company,
        double? SalaryMin,
        double? SalaryMax,
        string? SalaryIsPredicted,        // ✅ String ("0" or "1")
        string? ContractTime,             // ✅ e.g., "full_time"
        string? ContractType              // ✅ e.g., "permanent"
    );

    public record LocationDto
    (
        string DisplayName,
        List<string> Area
    );

    public record CategoryDto
    (
        string Tag,
        string Label
    );

    public record CompanyDto
    (
        string DisplayName,
        string? CanonicalName,
        int? Count,                       // ✅ Added
        double? AverageSalary            // ✅ Added
    );
}
