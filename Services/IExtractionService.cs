namespace Career_Path.Services
{
    public interface IExtractionService
    {
        Task<Result> GetExtractionAsync(string userId, IFormFile formFile, CancellationToken cancellationToken);
    }
}
