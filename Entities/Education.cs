namespace Career_Path.Entities
{
    [Owned]
    public class Education
    {
        public string Degree { get; set; } = string.Empty;
        public string Field { get; set; }= string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }
}
