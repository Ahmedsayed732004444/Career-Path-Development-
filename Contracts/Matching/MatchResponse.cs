namespace Career_Path.Contracts.Matching
{
    public class MatchResponse
    {
        public List<JobMatchDto> Matches { get; set; } = [];
    }

    public class JobMatchDto
    {
        public string JobTitle { get; set; } = string.Empty;
        public int MatchPercentage { get; set; }
        public List<string> MatchedSkills { get; set; } = [];
        public List<string> MissingSkills { get; set; } = [];
    }
}
