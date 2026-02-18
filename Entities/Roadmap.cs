namespace Career_Path.Entities
{
    public class Roadmap
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string CurrentDomain { get; set; } = string.Empty;
        public string CurrentLevel { get; set; } = string.Empty;
        public string TargetRole { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public string TransitionDifficulty { get; set; } = string.Empty;
        public bool IsValidTransition { get; set; }
        public string ValidationMessage { get; set; } = string.Empty;
        public string MermaidDiagram { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ApplicationUser ApplicationUser { get; set; } = default!;
        public List<RoadmapPhase> Phases { get; set; } = [];
        public List<ProjectImprovement> ProjectImprovements { get; set; } = [];
    }
}