using Career_Path.Authentication.Filters;
using Career_Path.Contracts.Common;
using Career_Path.Contracts.Job;
namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController(IJobService _jobService) : ControllerBase
    {

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetJobs)]
        public async Task<IActionResult> GetJobAsync(string id, CancellationToken cancellationToken)
        {
            var result = await _jobService.GetJobAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
        [HttpGet]
        [HasPermission(Permissions.GetJobs)]
        public async Task<IActionResult> GetJobsAsync([FromQuery]RequestFilters filters, CancellationToken cancellationToken)
        {
            var result = await _jobService.GetCombanyJobsAsync(User.GetUserId()!, filters, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
        [HttpPost]
        [HasPermission(Permissions.AddJobs)]
        public async Task<IActionResult> AddJobAsync([FromBody] JopRequest request, CancellationToken cancellationToken)
        {
            var result = await _jobService.AddJobAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess? Created($"/api/Jobs/{result.Value.Id}", result.Value): result.ToProblem();
        }
        [HttpPut("{IobId}")]
        [HasPermission(Permissions.UpdateJobs)]
        public async Task<IActionResult> UpdateJobAsync(string IobId, [FromBody] JopRequest request, CancellationToken cancellationToken)
        {
            var result = await _jobService.UpdateJobAsync(User.GetUserId()!, IobId, request, cancellationToken);
            return result.IsSuccess ? Created() : result.ToProblem();
        }

         [HttpDelete("{IobId}")]
        [HasPermission(Permissions.UpdateJobs)]
        public async Task<IActionResult> DeleteJobAsync(string IobId, CancellationToken cancellationToken)
         {
                var result = await _jobService.DeleteJobAsync(User.GetUserId()!, IobId, cancellationToken);
                return result.IsSuccess ? Ok() : result.ToProblem();
         }
        [HttpPut("{IobId}/toggle-status")]
        [HasPermission(Permissions.UpdateJobs)]
        public async Task<IActionResult> ToggleStatusAsync([FromRoute] string IobId,CancellationToken cancellationToken)
        {
            var result = await _jobService.ToggleStatusAsync(User.GetUserId()!, IobId, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> ApplyToJobAsync(string id,[FromForm] ApplyJobRequest request,CancellationToken cancellationToken)
        {
            var result = await _jobService.ApplayForJobAsync(User.GetUserId()!, id, request, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpGet("{id}/applicants")]
        [HasPermission(Permissions.GetJobApplicants)]
        public async Task<IActionResult> GetJobApplicantsAsync(string id, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
        {
            var result = await _jobService.GetJobApplicantsAsync(User.GetUserId()!, id,filters, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
    }
}
