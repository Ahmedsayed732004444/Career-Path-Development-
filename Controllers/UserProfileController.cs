using Career_Path.Contracts.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IUserProfileService userProfileService) : ControllerBase
    {
        private readonly IUserProfileService _userProfileService = userProfileService;

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            return Ok(await _userProfileService.GetAsync(User.GetUserId()!));
        }

        [HttpPut("basic-Info")]
        public async Task<IActionResult> UpdateBasicDaya([FromBody] BasicInfoRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateBasicInfoAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("skills")]
        public async Task<IActionResult> UpdateSoftSkills([FromBody] SkillsRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateSkillsAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("education")]
        public async Task<IActionResult> UpdateEducation([FromBody] EducationRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateEducationAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("summary")]
        public async Task<IActionResult> UpdateSummary([FromBody] SummaryRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateSummaryAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("cv")]
        public async Task<IActionResult> UpdateCv([FromForm] UpdateUserProfileCvRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateCvAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("picture")]
        public async Task<IActionResult> UpdatePicture([FromForm] UpdateUserProfilePictureRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdatePictureAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("cv")]
        public async Task<IActionResult> DeleteCv(CancellationToken cancellationToken)
        {
            var result = await _userProfileService.DeleteCvAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("picture")]
        public async Task<IActionResult> DeletePicture(CancellationToken cancellationToken)
        {
            var result = await _userProfileService.DeletePictureAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
