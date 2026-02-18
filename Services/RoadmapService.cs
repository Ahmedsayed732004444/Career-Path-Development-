using Career_Path.Contracts.Roadmap;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Career_Path.Services
{
    public class RoadmapService : IRoadmapService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ApplicationDbContext _context;

        public RoadmapService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["RoadmapApi:BaseUrl"] ?? "http://127.0.0.1:8000/";
            _context = context;
        }

        public async Task<Result<RoadmapResponse>> GenerateRoadmapAsync(string userId, string targetRole, CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if roadmap exists for this user and target role
                var existingRoadmap = await _context.Roadmaps.AsSplitQuery()
                    .Include(r => r.Phases)
                        .ThenInclude(p => p.Skills)
                    .Include(r => r.Phases)
                        .ThenInclude(p => p.Resources)
                    .Include(r => r.ProjectImprovements)
                    .FirstOrDefaultAsync(r => r.ApplicationUserId == userId && r.TargetRole == targetRole, cancellationToken);

                if (existingRoadmap is not null)
                {
                    // Return existing roadmap from database using Mapster
                    var existingResponse = existingRoadmap.Adapt<RoadmapResponse>();
                    return Result.Success(existingResponse);
                }

                // Generate new roadmap from API
                using var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("target_role", targetRole)
                });

                var response = await _httpClient.PostAsync($"{_baseUrl}api/v1/generate-roadmap/{userId}", content, cancellationToken);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Result.Failure<RoadmapResponse>(RoadmapErrors.GenerationFailed);
                }

                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var roadmapResponse = JsonSerializer.Deserialize<RoadmapResponse>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                });

                if (roadmapResponse is null)
                {
                    return Result.Failure<RoadmapResponse>(RoadmapErrors.GenerationFailed);
                }

                // Save to database using Mapster
                await SaveRoadmapToDbAsync(roadmapResponse, cancellationToken);

                return Result.Success(roadmapResponse);
            }
            catch (Exception ex)
            {
                return Result.Failure<RoadmapResponse>(RoadmapErrors.GenerationFailed);
            }
        }

        private async Task SaveRoadmapToDbAsync(RoadmapResponse response, CancellationToken cancellationToken)
        {
            var roadmap = response.Adapt<Roadmap>();
            _context.Roadmaps.Add(roadmap);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}