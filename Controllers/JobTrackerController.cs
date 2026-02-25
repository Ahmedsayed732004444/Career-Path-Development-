using Career_Path.Contracts.JobApplication;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobTrackerController(IJobApplicationService _jobApplicationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetJobTraces(CancellationToken cancellationToken)
        {
            return Ok(await _jobApplicationService.GetJobApplicationsUserAsync(User.GetUserId()!));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTrace(string id, CancellationToken cancellationToken)
        {
            var result = await _jobApplicationService.GetJobApplicationAsync(User.GetUserId()!, id, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobTrace(JobApplicationRequest request, CancellationToken cancellationToken)
        {
            var result = await _jobApplicationService.AddJobApplicationAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess? Created($"/api/JobTracker/{result.Value.Id}", result.Value): result.ToProblem();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobTrace(string id, JobApplicationRequest request, CancellationToken cancellationToken)
        {
            var result = await _jobApplicationService.UpdateJobApplicationAsync(User.GetUserId()!, id, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTrace(string id, CancellationToken cancellationToken)
        {
            var result = await _jobApplicationService.DeleteJobApplicationAsync(User.GetUserId()!, id, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

    }
}