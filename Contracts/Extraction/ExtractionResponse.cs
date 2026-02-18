namespace Career_Path.Contracts.Extraction
{
    public class ExtractionResponse
    {
        public PersonalInfo Personal_Info { get; set; }
        public string Summary { get; set; }
        public List<string> Skills { get; set; }
        public List<EducationDto> Education { get; set; }
        public List<ExperienceDto> Experience { get; set; }
        public List<string> Certifications { get; set; }
        public List<string> Languages { get; set; }
    }

    public class PersonalInfo
    {
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
    }

    public class EducationDto
    {
        public string Degree { get; set; }
        public string Field { get; set; }
        public string Institution { get; set; }
        public string Year { get; set; }
    }

    public class ExperienceDto
    {
        public string Job_Title { get; set; }
        public string Company { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Description { get; set; }
    }

}
