namespace Career_Path.Contracts.Matching
{
    public class MatchRequest
    {
        public List<string> UserSkills { get; set; } = [];
        public List<JobRequestDto> Jobs { get; set; } = [];
    }

    public class JobRequestDto
    {
        public string JobTitle { get; set; } = string.Empty;
        public List<string> JobRequirements { get; set; } = [];
    }
}
