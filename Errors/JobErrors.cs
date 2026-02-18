namespace Career_Path.Errors
{
    public static class JobErrors
    {
        public static readonly Error JobNotFound =
            new("Job.JobNotFound", "Job not found", StatusCodes.Status404NotFound);

        public static readonly Error Unauthorized =
            new("Job.Unauthorized", "You are not authorized to perform this action", StatusCodes.Status403Forbidden);

        public static readonly Error CompanyNotFound =
            new("Job.CompanyNotFound", "Company not found", StatusCodes.Status404NotFound);

        public static readonly Error InvalidJobData =
            new("Job.InvalidData", "Invalid job data provided", StatusCodes.Status400BadRequest);

        public static readonly Error JobAlreadyInactive =
            new("Job.AlreadyInactive", "Job is already inactive", StatusCodes.Status400BadRequest);
    }
}