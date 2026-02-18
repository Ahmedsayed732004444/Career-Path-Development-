using Career_Path.Contracts.Matching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AiController(
        IRoadmapService _roadmapService,
        IMatchService _matchService) : ControllerBase
    {
        [HttpGet("roadmap")]
        public async Task<IActionResult> GetRoadmapAsync([FromQuery] string targetRole)
        {
            var result = await _roadmapService.GenerateRoadmapAsync(User.GetUserId()!, targetRole);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpGet("match")]
        public async Task<IActionResult> MatchUserWithJobsAsync()
        {
            var result = await _matchService.MatchUserWithJobsAsync(User.GetUserId()!);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpGet("match/top")]
        public async Task<IActionResult> GetTopMatchesAsync([FromQuery] int topN = 10)
        {
            var result = await _matchService.GetTopMatchesAsync(User.GetUserId()!, topN);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpGet("match/filter")]
        public async Task<IActionResult> GetMatchesByPercentageAsync([FromQuery] int minPercentage = 50)
        {
            var result = await _matchService.GetMatchesByPercentageAsync(User.GetUserId()!, minPercentage);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
    }
}