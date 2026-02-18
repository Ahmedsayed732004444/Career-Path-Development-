using Career_Path.Contracts.JopSearch;

namespace Career_Path.Services
{
    public interface IJobSearchService
    {
        Task<Result<JobSearchResponse>> SearchJobsAsync(JobSearchRequest request, CancellationToken cancellationToken = default);
        Task<Result<CategoryResponse>> GetCategoriesAsync(string country, CancellationToken cancellationToken = default);
    }
}
