using Career_Path.Contracts.JobApplication;

namespace Career_Path.Services;

public interface IJobApplicationService
{
    Task<List<JobApplicationRespons>> GetJobApplicationsUserAsync(string userId);
    Task<Result> GetJobApplicationAsync(string userId, string jobApplicationId, CancellationToken cancellationToken);
    Task<Result> DeleteJobApplicationAsync(string userId,string jobApplicationId, CancellationToken cancellationToken);
    Task<Result<JobApplicationRespons>> AddJobApplicationAsync(string userId, JobApplicationRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateJobApplicationAsync(string userId,string jobApplicationId, JobApplicationRequest request, CancellationToken cancellationToken);
}
