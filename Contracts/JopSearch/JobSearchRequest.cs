namespace Career_Path.Contracts.JopSearch
{
    public record JobSearchRequest
    (
        string? What = null,              // Keywords
        string? WhatAnd = null,           // All keywords must exist
        string? WhatPhrase = null,        // Exact phrase
        string? WhatOr = null,            // Any keyword
        string? WhatExclude = null,       // Exclude keywords
        string? TitleOnly = null,         // Search in title only
        string? Where = null,             // Location
        int? Distance = null,             // Distance in km
        string? Category = null,          // Category tag
        int? SalaryMin = null,            // Minimum salary
        int? SalaryMax = null,            // Maximum salary
        string? SalaryIncludeUnknown = null, // Include jobs without salary
        string? FullTime = null,          // Full time jobs only
        string? PartTime = null,          // Part time jobs only
        string? Contract = null,          // Contract jobs only
        string? Permanent = null,         // Permanent jobs only
        string? Company = null,           // Company name
        int? MaxDaysOld = null,           // Maximum age in days
        string? SortBy = null,            // Sort field
        string? SortDir = null,           // Sort direction (asc/desc)
        int ResultsPerPage = 20,          // Results per page
        int Page = 1                      // Page number
    );
}
