using Career_Path.Contracts.UserProfile;
using Microsoft.EntityFrameworkCore;

namespace Career_Path.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> GetAsync(string applicationUserId);
        Task<Result> UpdateBasicInfoAsync(string applicationUserId, BasicInfoRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateCvAsync(string applicationUserId, UpdateUserProfileCvRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdatePictureAsync(string applicationUserId, UpdateUserProfilePictureRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateEducationAsync(string applicationUserId, EducationRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateSummaryAsync(string applicationUserId, SummaryRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateSkillsAsync(string applicationUserId, SkillsRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteCvAsync(string applicationUserId, CancellationToken cancellationToken = default);
        Task<Result> DeletePictureAsync(string applicationUserId, CancellationToken cancellationToken = default);
    }
}
