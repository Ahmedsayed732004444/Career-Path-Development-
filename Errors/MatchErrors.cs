namespace Career_Path.Errors
{
    public static class MatchErrors
    {
        public static readonly Error UserSkillsNotFound =
            new("Match.UserSkillsNotFound", "User skills not found", StatusCodes.Status400BadRequest);

        public static readonly Error NoActiveJobs =
            new("Match.NoActiveJobs", "No active jobs available", StatusCodes.Status400BadRequest);

        public static readonly Error MatchingFailed =
            new("Match.MatchingFailed", "Failed to match skills with jobs", StatusCodes.Status400BadRequest);
    }
}
