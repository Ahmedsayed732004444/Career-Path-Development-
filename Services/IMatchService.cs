// Contracts/Matching/IMatchService.cs
namespace Career_Path.Contracts.Matching
{
    public interface IMatchService
    {
        Task<Result<MatchResponse>> MatchUserWithJobsAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<List<JobMatchDto>>> GetTopMatchesAsync(string userId, int topN = 10, CancellationToken cancellationToken = default);
        Task<Result<List<JobMatchDto>>> GetMatchesByPercentageAsync(string userId, int minPercentage = 50, CancellationToken cancellationToken = default);
    }
}