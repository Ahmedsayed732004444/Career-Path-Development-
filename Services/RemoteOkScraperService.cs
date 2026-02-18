using Career_Path.Contracts.JopSearch;
using System.Text.Json;

namespace Career_Path.Services
{
    public class RemoteOkScraperService : IRemoteOkScraperService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://remoteok.com/api";

        public RemoteOkScraperService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<Result<RemoteOkJobResponse>> SearchJobsAsync(RemoteOkSearchRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Add delay to avoid rate limiting
                await Task.Delay(1000, cancellationToken);

                var response = await _httpClient.GetAsync(ApiUrl, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Failure<RemoteOkJobResponse>(RemoteOkErrors.ScrapingFailed);
                }

                using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var allJobs = await JsonSerializer.DeserializeAsync<List<RemoteOkJobDto>>(stream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }, cancellationToken);

                if (allJobs == null || !allJobs.Any())
                {
                    return Result.Success(new RemoteOkJobResponse(new List<RemoteOkJobDto>()));
                }

                // Remove first item (it's metadata)
                allJobs = allJobs.Skip(1).ToList();

                // Filter jobs based on search criteria
                var filteredJobs = FilterJobs(allJobs, request);

                var jobResponse = new RemoteOkJobResponse(filteredJobs);
                return Result.Success(jobResponse);
            }
            catch (Exception ex)
            {
                return Result.Failure<RemoteOkJobResponse>(RemoteOkErrors.ScrapingFailed);
            }
        }

        private List<RemoteOkJobDto> FilterJobs(List<RemoteOkJobDto> jobs, RemoteOkSearchRequest request)
        {
            var filtered = jobs.AsEnumerable();

            // Filter by search keyword
            if (!string.IsNullOrEmpty(request.Search))
            {
                var searchLower = request.Search.ToLower();
                filtered = filtered.Where(j =>
                    (j.Title?.ToLower().Contains(searchLower) ?? false) ||
                    (j.Description?.ToLower().Contains(searchLower) ?? false) ||
                    (j.Tags?.Any(t => t.ToLower().Contains(searchLower)) ?? false)
                );
            }

            // Filter by tag
            if (!string.IsNullOrEmpty(request.Tag))
            {
                var tagLower = request.Tag.ToLower();
                filtered = filtered.Where(j =>
                    j.Tags?.Any(t => t.ToLower().Contains(tagLower)) ?? false
                );
            }

            return filtered.ToList();
        }
    }
}