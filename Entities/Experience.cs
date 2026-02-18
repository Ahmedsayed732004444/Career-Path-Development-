namespace Career_Path.Entities
{
    [Owned]
    public class Experience
    {
        public string JobTitle { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
