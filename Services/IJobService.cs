using Career_Path.Contracts.Common;
using Career_Path.Contracts.Job;
using Microsoft.EntityFrameworkCore;

namespace Career_Path.Services
{
    public interface IJobService
    {
        Task<Result<JobResponse>> AddJobAsync(string CompanyId, JopRequest request, CancellationToken cancellationToken);

        Task<Result> UpdateJobAsync(string companyId, string jobId, JopRequest request, CancellationToken cancellationToken);

        Task<Result> DeleteJobAsync(string companyId, string jobId, CancellationToken cancellationToken);

        Task<Result> ToggleStatusAsync(string companyId, string jobId, CancellationToken cancellationToken);


        Task<Result<JobResponse>> GetJobAsync(string jobId, CancellationToken cancellationToken);

        Task<Result<PaginatedList<JobResponse>>> GetCombanyJobsAsync(string CompanyId, RequestFilters filters, CancellationToken cancellationToken = default);
        Task<Result> ApplayForJobAsync(string userId, string jobId, ApplyJobRequest request, CancellationToken cancellationToken);

        Task<Result<PaginatedList<ApplyJobResponse>>> GetJobApplicantsAsync(string companyId, string jobId, RequestFilters filters, CancellationToken cancellationToken = default);
    }
}
