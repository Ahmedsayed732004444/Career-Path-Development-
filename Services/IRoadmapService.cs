using Career_Path.Contracts.Roadmap;

namespace Career_Path.Services
{
    public interface IRoadmapService
    {
        Task<Result<RoadmapResponse>> GenerateRoadmapAsync(string userId, string targetRole, CancellationToken cancellationToken = default);
    }
}