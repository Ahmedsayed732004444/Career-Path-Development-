namespace Career_Path.Contracts.UserProfile
{
    public record UserProfileResponse
    (
        string FullName,
        string Email,
        string? ProfilePictureUrl,
        UserGender? Gender,
        string? JobTitle,
        string? Country,
        string? City,   
        string? University,
        string? CurrentCompany,
        string? Degree,
        int? YearsOfExperience,
        string? Summary,
        int? GraduationYear,
        string? CvFileUrl,
        ICollection<string> Skills
    );
}
