namespace Career_Path.Entities
{
    public enum UserGender
    {
        Male,
        Female
    }

    public class UserProfile
    {
        public int ID { get; set; } 

        public string ApplicationUserId { get; set; }

        // Basic Info
        public UserGender? Gender { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        // Career
        public string? JobTitle { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? CurrentCompany { get; set; }

        // Education
        public string? University { get; set; }
        public string? Degree { get; set; }
        public int? GraduationYear { get; set; }

        // Profile
        public string? Summary { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CvFileUrl { get; set; }

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ApplicationUser ApplicationUser { get; set; }
    }
}