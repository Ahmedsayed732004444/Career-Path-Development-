using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JopsearchController(IJobSearchService _jobSearchService,IRemoteOkScraperService _remoteOkScraperService) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> SearchJobs([FromQuery] JobSearchRequest request, CancellationToken cancellationToken)
        {
            var result = await _jobSearchService.SearchJobsAsync(request, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("categories/{country}")]
        public async Task<IActionResult> GetCategories(string country, CancellationToken cancellationToken)
        {
            var result = await _jobSearchService.GetCategoriesAsync(country, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("remoteok/search")]
        public async Task<IActionResult> SearchRemoteOkJobs([FromQuery] RemoteOkSearchRequest request, CancellationToken cancellationToken)
        {
            var result = await _remoteOkScraperService.SearchJobsAsync(request, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
