using Career_Path.Contracts.JopSearch;
using System.Text.Json;
using System.Web;

namespace Career_Path.Services
{
    public class JobSearchService : IJobSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _appId;
        private readonly string _appKey;

        public JobSearchService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["AdzunaApi:BaseUrl"] ?? "https://api.adzuna.com/v1/api";
            _appId = configuration["AdzunaApi:AppId"] ?? throw new ArgumentNullException("AppId is required");
            _appKey = configuration["AdzunaApi:AppKey"] ?? throw new ArgumentNullException("AppKey is required");
        }

        public async Task<Result<JobSearchResponse>> SearchJobsAsync(JobSearchRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var queryParams = BuildSearchQuery(request);
                var url = $"{_baseUrl}/jobs/gb/search/{request.Page}?app_id={_appId}&app_key={_appKey}&{queryParams}";

                var response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Result.Failure<JobSearchResponse>(JobSearchErrors.SearchFailed);
                }

                using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var jobSearchResponse = await JsonSerializer.DeserializeAsync<JobSearchResponse>(stream, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                }, cancellationToken);

                if (jobSearchResponse is null)
                {
                    return Result.Failure<JobSearchResponse>(JobSearchErrors.SearchFailed);
                }

                return Result.Success(jobSearchResponse);
            }
            catch (Exception ex)
            {
                return Result.Failure<JobSearchResponse>(JobSearchErrors.SearchFailed);
            }
        }

        public async Task<Result<CategoryResponse>> GetCategoriesAsync(string country, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrl}/jobs/{country}/categories?app_id={_appId}&app_key={_appKey}";

                var response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Result.Failure<CategoryResponse>(JobSearchErrors.CategoriesFetchFailed);
                }

                using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var categoryResponse = await JsonSerializer.DeserializeAsync<CategoryResponse>(stream, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                }, cancellationToken);

                if (categoryResponse is null)
                {
                    return Result.Failure<CategoryResponse>(JobSearchErrors.CategoriesFetchFailed);
                }

                return Result.Success(categoryResponse);
            }
            catch (Exception ex)
            {
                return Result.Failure<CategoryResponse>(JobSearchErrors.CategoriesFetchFailed);
            }
        }

        private string BuildSearchQuery(JobSearchRequest request)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrEmpty(request.What)) query["what"] = request.What;
            if (!string.IsNullOrEmpty(request.WhatAnd)) query["what_and"] = request.WhatAnd;
            if (!string.IsNullOrEmpty(request.WhatPhrase)) query["what_phrase"] = request.WhatPhrase;
            if (!string.IsNullOrEmpty(request.WhatOr)) query["what_or"] = request.WhatOr;
            if (!string.IsNullOrEmpty(request.WhatExclude)) query["what_exclude"] = request.WhatExclude;
            if (!string.IsNullOrEmpty(request.TitleOnly)) query["title_only"] = request.TitleOnly;
            if (!string.IsNullOrEmpty(request.Where)) query["where"] = request.Where;
            if (request.Distance.HasValue) query["distance"] = request.Distance.Value.ToString();
            if (!string.IsNullOrEmpty(request.Category)) query["category"] = request.Category;
            if (request.SalaryMin.HasValue) query["salary_min"] = request.SalaryMin.Value.ToString();
            if (request.SalaryMax.HasValue) query["salary_max"] = request.SalaryMax.Value.ToString();
            if (!string.IsNullOrEmpty(request.SalaryIncludeUnknown)) query["salary_include_unknown"] = request.SalaryIncludeUnknown;
            if (!string.IsNullOrEmpty(request.FullTime)) query["full_time"] = request.FullTime;
            if (!string.IsNullOrEmpty(request.PartTime)) query["part_time"] = request.PartTime;
            if (!string.IsNullOrEmpty(request.Contract)) query["contract"] = request.Contract;
            if (!string.IsNullOrEmpty(request.Permanent)) query["permanent"] = request.Permanent;
            if (!string.IsNullOrEmpty(request.Company)) query["company"] = request.Company;
            if (request.MaxDaysOld.HasValue) query["max_days_old"] = request.MaxDaysOld.Value.ToString();
            if (!string.IsNullOrEmpty(request.SortBy)) query["sort_by"] = request.SortBy;
            if (!string.IsNullOrEmpty(request.SortDir)) query["sort_dir"] = request.SortDir;

            query["results_per_page"] = request.ResultsPerPage.ToString();

            return query.ToString() ?? string.Empty;
        }
    }
}