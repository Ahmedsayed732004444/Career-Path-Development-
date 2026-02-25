using Career_Path.Contracts.JobApplication;
using Intelligent_Career_Advisor.Models;

namespace Career_Path.Services;

public class JobApplicationService(ApplicationDbContext context) : IJobApplicationService
{

    private readonly ApplicationDbContext _context = context;

    public async Task<Result> GetJobApplicationAsync(string userId, string id, CancellationToken cancellationToken)
    {
        var jobApplication = await _context.JobApplications.AsNoTracking()
            .FirstOrDefaultAsync(ja => ja.Id == id && ja.ApplicationUserId == userId, cancellationToken);
        if(jobApplication is null)
            return Result.Failure(JobErrors.JobNotFound);
        return  Result.Success( jobApplication?.Adapt<JobApplicationRespons>());
    }

    public async Task<List<JobApplicationRespons>> GetJobApplicationsUserAsync(string userId)
    {
        return await _context.JobApplications.AsNoTracking()
            .Where(ja => ja.ApplicationUserId == userId)
            .ProjectToType<JobApplicationRespons>()
            .ToListAsync();
    }

    public async Task<Result<JobApplicationRespons>> AddJobApplicationAsync(string userId, JobApplicationRequest request, CancellationToken cancellationToken)
    {
        var jobApplication = request.Adapt<JobApplication>();
        jobApplication.ApplicationUserId = userId;

        await _context.JobApplications.AddAsync(jobApplication, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(jobApplication.Adapt<JobApplicationRespons>());
    }
    

    public async Task<Result> DeleteJobApplicationAsync(string userId,string jobApplicationId, CancellationToken cancellationToken)
    {
        var jobApplication = await _context.JobApplications.
            FirstOrDefaultAsync(ja => ja.Id == jobApplicationId && ja.ApplicationUserId == userId, cancellationToken);
        if (jobApplication is null)
            return Result.Failure(JobErrors.JobNotFound);

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
    }

    public async Task<Result> UpdateJobApplicationAsync(string userId,string jobApplicationId, JobApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var jobApplication = await _context.JobApplications.
            FirstOrDefaultAsync(ja => ja.Id == jobApplicationId && ja.ApplicationUserId == userId, cancellationToken);
        if (jobApplication is null)
            return Result.Failure(JobErrors.JobNotFound);
        request.Adapt(jobApplication);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}
