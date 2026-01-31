using Career_Path.Contracts.UserProfile;
using Career_Path.Entities;
using Microsoft.AspNetCore.Identity;

namespace Career_Path.Services
{
    public class UserProfileService(
        ApplicationDbContext context, ILogger<UserProfileService> logger,
        IWebHostEnvironment env, IHttpContextAccessor accessor) : IUserProfileService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<UserProfileService> _logger = logger;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IWebHostEnvironment _env = env;

        public async Task<UserProfileResponse> GetAsync(string applicationUserId)
        {
            var user = await _context.UserProfiles.
                   Include(up => up.ApplicationUser).Include(up => up.Skills).FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            return user.Adapt<UserProfileResponse>();
        }

        public async Task<Result> UpdateBasicInfoAsync(string applicationUserId, BasicInfoRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.Include(up => up.ApplicationUser).
                FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            request.Adapt(userProfile);
            if(request.FirstName is not null)
            userProfile.ApplicationUser.FirstName = request.FirstName;
            if (request.LastName is not null)
            userProfile.ApplicationUser.LastName = request.LastName;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> UpdateEducationAsync(string applicationUserId, EducationRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId, cancellationToken);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            request.Adapt(userProfile);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task <Result> UpdateSummaryAsync(string applicationUserId, SummaryRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId, cancellationToken);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            userProfile.Summary = request.Summary;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateSkillsAsync(string applicationUserId, SkillsRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.Include(up => up.Skills)
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId, cancellationToken);

            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            var newSkills = (request.Skills ?? new List<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            var existingSkills = userProfile.Skills.ToList();

            var skillsToRemove = existingSkills.Where(es => !newSkills.Contains(es.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            var skillsToAdd = newSkills.Where(ns => !existingSkills.Any(es =>
                    es.Name.Equals(ns, StringComparison.OrdinalIgnoreCase)))
                .Select(skillName => new Skill {Name = skillName,UserProfileId = userProfile.ID}).ToList();

            if (!skillsToRemove.Any() && !skillsToAdd.Any())
                return Result.Success();

            if (skillsToRemove.Any())
                _context.Skills.RemoveRange(skillsToRemove);

            if (skillsToAdd.Any())
                await _context.Skills.AddRangeAsync(skillsToAdd, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> UpdateCvAsync(string applicationUserId, UpdateUserProfileCvRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId, cancellationToken);

            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            if (!string.IsNullOrEmpty(userProfile.CvFileUrl))
            {
                FileHelper.DeleteFile(userProfile.CvFileUrl, "CvS", _env);
            }
            userProfile.CvFileUrl = await FileHelper.UploadeFileAsync(request.CvFile, "CvS", _env, _accessor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> UpdatePictureAsync(string applicationUserId, UpdateUserProfilePictureRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            if (!string.IsNullOrEmpty(userProfile.ProfilePictureUrl))
            {
                FileHelper.DeleteFile(userProfile.ProfilePictureUrl, "Images", _env);
            }
            userProfile.ProfilePictureUrl = await FileHelper.UploadeFileAsync(request.ProfilePicture, "Images", _env, _accessor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteCvAsync(string applicationUserId, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            if (userProfile.CvFileUrl is null)
                return Result.Failure(UserErrors.FileNotFound);
            FileHelper.DeleteFile(userProfile.CvFileUrl, "CvS", _env);
            userProfile.CvFileUrl = null;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> DeletePictureAsync(string applicationUserId, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            if (userProfile.ProfilePictureUrl is null)
                return Result.Failure(UserErrors.FileNotFound);
            FileHelper.DeleteFile(userProfile.ProfilePictureUrl, "Images", _env);
            userProfile.ProfilePictureUrl = null;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
