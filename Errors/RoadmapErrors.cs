namespace Career_Path.Errors
{
    public record class RoadmapErrors
    {
        public static readonly Error GenerationFailed = new("Roadmap.GenerationFailed", "Failed to generate roadmap", StatusCodes.Status400BadRequest);
    }
}
