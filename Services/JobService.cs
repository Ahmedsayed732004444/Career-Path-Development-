using Career_Path.Contracts.Common;
using Career_Path.Contracts.Job;
using System.Linq.Dynamic.Core;

namespace Career_Path.Services
{
    public class JobService(
        ApplicationDbContext context, ILogger<JobService> logger,
        IWebHostEnvironment env, IHttpContextAccessor accessor, IExtractionService extractionService)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<JobService> _logger = logger;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IWebHostEnvironment _env = env;
        private readonly IExtractionService _extractionService = extractionService;


        public async Task<Result> AddJobAsync(string CompanyId, JopRequest request, CancellationToken cancellationToken)
        {
            var job = request.Adapt<Job>(); 
            job.CompanyId = CompanyId;
            await _context.Jobs.AddAsync(job, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateJobAsync(string companyId, int jobId, JopRequest request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);

            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);

            request.Adapt(job);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteJobAsync(string companyId, int jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);
            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(string companyId, int jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == jobId && j.CompanyId == companyId, cancellationToken);
            if (job is null)
                return Result.Failure(JobErrors.JobNotFound);
            job.IsActive = !job.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        
        public async Task<Result<JobResponse>> GetJobAsync(int jobId, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.FindAsync(jobId, cancellationToken);
            if (job is null)
            {
                return Result.Failure<JobResponse>(JobErrors.JobNotFound);
            }
            var response = job.Adapt<JobResponse>();
            return Result.Success(response);
        }
        public async Task<Result<PaginatedList<JobResponse>>> GetCombanyJobsAsync(string CompanyId, RequestFilters filters, CancellationToken cancellationToken = default)
        {
            var JobsExists = await _context.Jobs.AnyAsync(j => j.CompanyId == CompanyId, cancellationToken: cancellationToken);

            if (!JobsExists)
                return Result.Failure<PaginatedList<JobResponse>>(JobErrors.JobNotFound);
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
                        .ProjectToType<JobResponse>()
                        .AsNoTracking();

            var Jobs = await PaginatedList<JobResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

            return Result.Success(Jobs);

        }
    }
}

