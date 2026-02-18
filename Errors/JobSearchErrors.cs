namespace Career_Path.Errors
{
    public record class JobSearchErrors
    {
        public static readonly Error SearchFailed = new("JobSearch.SearchFailed", "Failed to search jobs", StatusCodes.Status400BadRequest);
        public static readonly Error CategoriesFetchFailed = new("JobSearch.CategoriesFetchFailed", "Failed to fetch categories", StatusCodes.Status400BadRequest);
    }
}
