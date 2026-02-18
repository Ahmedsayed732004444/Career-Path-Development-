namespace Career_Path.Contracts.Roadmap
{
    public record RoadmapResponse
    (
        string UserId,
        string CurrentDomain,
        string CurrentLevel,
        string TargetRole,
        int DurationMonths,
        string TransitionDifficulty,
        bool IsValidTransition,
        string ValidationMessage,
        List<PhaseDto> Phases,
        List<string> ProjectImprovements,
        string MermaidDiagram
    );

    public record PhaseDto
    (
        int Month,
        string FocusArea,
        List<string> SkillsToLearn,
        List<string> Resources
    );
}