using Career_Path.Contracts.Extraction;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Career_Path.Services
{
    public class ExtractionService : IExtractionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExtractionService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            //_baseUrl = configuration["ExtractionApi:BaseUrl"] ?? "https://cv-parser-production-5981.up.railway.app/";
            _baseUrl = configuration["ExtractionApi:BaseUrl"] ?? "http://127.0.0.1:8000/";

        }

        public async Task<Result> GetExtractionAsync(string userId, IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                using var formData = new MultipartFormDataContent();
                using var fileStream = formFile.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);

                formData.Add(fileContent, "file", formFile.FileName);
                formData.Add(new StringContent(userId), "application_user_id");

                var response = await _httpClient.PostAsync($"{_baseUrl}api/v1/cv-box", formData, cancellationToken);

                // لو رجع 200 يبقى السيرفر خزنها في الداتابيز
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return Result.Failure(ExtractionErrors.ExtractionFailed);
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ExtractionErrors.ExtractionFailed);
            }
        }
    }
}