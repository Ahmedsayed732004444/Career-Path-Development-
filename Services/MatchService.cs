using Career_Path.Contracts.Matching;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Career_Path.Services
{
    public class MatchService : IMatchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ApplicationDbContext _context;

        public MatchService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["RoadmapApi:BaseUrl"] ?? "http://127.0.0.1:8000/";
            _context = context;
        }

        public async Task<Result<MatchResponse>> MatchUserWithJobsAsync(
            string userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Get user skills from ModelExtraction
                var userExtraction = await _context.ModelExtrations
                    .FirstOrDefaultAsync(m => m.ApplicationUserId == userId, cancellationToken);

                if (userExtraction is null || !userExtraction.Skills.Any())
                {
                    return Result.Failure<MatchResponse>(MatchErrors.UserSkillsNotFound);
                }

                // Get active jobs from database
                var activeJobs = await _context.Jobs
                    .Where(j => j.IsActive)
                    .Select(j => new JobRequestDto
                    {
                        JobTitle = j.JobTitle,
                        JobRequirements = j.JobRequirements
                    })
                    .ToListAsync(cancellationToken);

                if (!activeJobs.Any())
                {
                    return Result.Failure<MatchResponse>(MatchErrors.NoActiveJobs);
                }

                // Prepare request body
                var matchRequest = new MatchRequest
                {
                    UserSkills = userExtraction.Skills,
                    Jobs = activeJobs
                };

                var jsonContent = JsonSerializer.Serialize(matchRequest, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                });

                using var content = new StringContent(
                    jsonContent,
                    System.Text.Encoding.UTF8,
                    "application/json");

                // Call matching API
                var response = await _httpClient.PostAsync(
                    $"{_baseUrl}api/v1/matching",
                    content,
                    cancellationToken);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Result.Failure<MatchResponse>(MatchErrors.MatchingFailed);
                }

                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var matchResponse = JsonSerializer.Deserialize<MatchResponse>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                });

                if (matchResponse is null)
                {
                    return Result.Failure<MatchResponse>(MatchErrors.MatchingFailed);
                }

                return Result.Success(matchResponse);
            }
            catch (Exception ex)
            {
                return Result.Failure<MatchResponse>(MatchErrors.MatchingFailed);
            }
        }

        public async Task<Result<List<JobMatchDto>>> GetTopMatchesAsync(
            string userId,
            int topN = 10,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var matchResult = await MatchUserWithJobsAsync(userId, cancellationToken);

                if (matchResult.IsFailure)
                {
                    return Result.Failure<List<JobMatchDto>>(matchResult.Error);
                }

                var topMatches = matchResult.Value.Matches
                    .OrderByDescending(m => m.MatchPercentage)
                    .Take(topN)
                    .ToList();

                return Result.Success(topMatches);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<JobMatchDto>>(MatchErrors.MatchingFailed);
            }
        }

        public async Task<Result<List<JobMatchDto>>> GetMatchesByPercentageAsync(
            string userId,
            int minPercentage = 50,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var matchResult = await MatchUserWithJobsAsync(userId, cancellationToken);

                if (matchResult.IsFailure)
                {
                    return Result.Failure<List<JobMatchDto>>(matchResult.Error);
                }

                var filteredMatches = matchResult.Value.Matches
                    .Where(m => m.MatchPercentage >= minPercentage)
                    .OrderByDescending(m => m.MatchPercentage)
                    .ToList();

                return Result.Success(filteredMatches);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<JobMatchDto>>(MatchErrors.MatchingFailed);
            }
        }
    }
}