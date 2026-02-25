using Career_Path.Contracts.Common;
using Career_Path.Contracts.Job;
using System.Data;
using System.Linq.Dynamic.Core;

namespace Career_Path.Services
{
    public class JobService(
        ApplicationDbContext context, IHttpContextAccessor accessor, IWebHostEnvironment env ) : IJobService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IWebHostEnvironment _env = env;


        public async Task<Result<JobResponse>> AddJobAsync(string CompanyId, JopRequest request, CancellationToken cancellationToken)
        {
            var job = request.Adapt<Job>();
            job.Id = Guid.CreateVersion7().ToString();
            job.CompanyId = CompanyId;

            await _context.Jobs.AddAsync(job, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var jsonresponse = job.Adapt<JobResponse>();
            return Result.Success(jsonresponse);
        }

        public async Task<Result> UpdateJobAsync(string companyId, string jobId, JopRequest request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);

            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);

            request.Adapt(job);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteJobAsync(string companyId, string jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);
            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(string companyId, string jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);
            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);
            job.IsActive = !job.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<JobResponse>> GetJobAsync(string jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == jobId, cancellationToken);
            if (job is null)
                return Result.Failure<JobResponse>(JobErrors.JobNotFound);
            var response = job.Adapt<JobResponse>();
            return Result.Success(response);
        }
        public async Task<Result<PaginatedList<JobResponse>>> GetCombanyJobsAsync(string CompanyId, RequestFilters filters, CancellationToken cancellationToken = default)
        {
            var query = _context.Jobs
                .Where(j => j.CompanyId == CompanyId);
            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.JobTitle.Contains(filters.SearchValue));
            }

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            var source = query
                        .AsNoTracking()
                        .ProjectToType<JobResponse>();

            var Jobs = await PaginatedList<JobResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

            return Result.Success(Jobs);

        }
        public async Task<Result> ApplayForJobAsync(string userId,string jobId,ApplyJobRequest request,CancellationToken cancellationToken = default)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId, cancellationToken);

            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);

            if (!job.IsActive||(job.DeadlineDate.HasValue && job.DeadlineDate < DateTime.UtcNow))
                return Result.Failure(JobErrors.JobClosed);

            var alreadyApplied = await _context.JobSubmissions
                .AnyAsync(js => js.JobId == jobId && js.ApplicantId == userId, cancellationToken);

            if (alreadyApplied)
                return Result.Failure(JobErrors.AlreadyApplied);

            var submission = request.Adapt<JobSubmission>();

            submission.CVPath = await FileHelper.UploadeFileAsync(
                request.CV, "CvSApplay", _env, _accessor);

            submission.ApplicantId = userId;
            submission.JobId = jobId;

            await _context.JobSubmissions.AddAsync(submission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result<PaginatedList<ApplyJobResponse>>> GetJobApplicantsAsync(string companyId, string jobId, RequestFilters filters, CancellationToken cancellationToken = default)
        {
            var jobExists = await _context.Jobs
                .AnyAsync(j => j.CompanyId == companyId && j.Id == jobId, cancellationToken);

            if (!jobExists)
                return Result.Failure<PaginatedList<ApplyJobResponse>>(JobErrors.JobNotFound);

            var query = _context.JobSubmissions
                .Where(js => js.JobId == jobId);

            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.Notes != null &&x.Notes.Contains(filters.SearchValue));
            }

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }

            var source = query
                .ProjectToType<ApplyJobResponse>()
                .AsNoTracking();

            var submissions = await PaginatedList<ApplyJobResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);
            return Result.Success(submissions);
        }

    }
}

