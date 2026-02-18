namespace Career_Path.Entities
{
    public class ModelExtration
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FullName { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string Phone { get; set; }= string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = [];
        public List<Education> Education { get; set; }= [];
        public List<Experience> Experience { get; set; } = [];
        public List<string> Certifications { get; set; }= [];
        public List<string> Languages { get; set; }= [];
        public virtual ApplicationUser  ApplicationUser { get; set; }   
    }
}
