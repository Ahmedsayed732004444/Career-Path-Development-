namespace Career_Path.Entities
{
    public class PhaseResource
    {
        public int Id { get; set; }
        public int RoadmapPhaseId { get; set; }  // ✅ غيرت من PhaseId
        public string Url { get; set; } = string.Empty;

        // Navigation Property
        public RoadmapPhase RoadmapPhase { get; set; } = default!;  // ✅ غيرت من Phase
    }
}