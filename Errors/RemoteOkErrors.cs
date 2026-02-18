namespace Career_Path.Errors
{
    public record class RemoteOkErrors
    {
        public static readonly Error ScrapingFailed = new("RemoteOk.ScrapingFailed", "Failed to scrape jobs from RemoteOK", StatusCodes.Status400BadRequest);
    }
}
