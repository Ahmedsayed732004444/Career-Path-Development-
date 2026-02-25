namespace Career_Path.Entities
{
    public class ProjectImprovement  
    {
        public int Id { get; set; }
        public int RoadmapId { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation Property
        public Roadmap Roadmap { get; set; } = default!;
    }
}