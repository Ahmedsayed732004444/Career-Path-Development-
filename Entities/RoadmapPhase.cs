namespace Career_Path.Entities
{
    public class RoadmapPhase  
    {
        public int Id { get; set; }
        public int RoadmapId { get; set; }
        public int Month { get; set; }
        public string FocusArea { get; set; } = string.Empty;

        // Navigation Properties
        public Roadmap Roadmap { get; set; } = default!;
        public List<PhaseSkill> Skills { get; set; } = [];
        public List<PhaseResource> Resources { get; set; } = [];
    }
}