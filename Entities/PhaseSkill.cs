namespace Career_Path.Entities
{
    public class PhaseSkill
    {
        public int Id { get; set; }
        public int RoadmapPhaseId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public RoadmapPhase RoadmapPhase { get; set; } = default!;
    }
}