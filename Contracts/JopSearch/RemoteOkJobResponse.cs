using System.Text.Json.Serialization;

namespace Career_Path.Contracts.JopSearch
{
    public record RemoteOkJobResponse
     (
         List<RemoteOkJobDto> Jobs
     );

    public record RemoteOkJobDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("slug")]
        public string? Slug { get; init; }

        [JsonPropertyName("position")]
        public string? Title { get; init; }

        [JsonPropertyName("company")]
        public string? Company { get; init; }

        [JsonPropertyName("company_logo")]
        public string? CompanyLogo { get; init; }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("url")]
        public string? Url { get; init; }

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; init; }

        [JsonPropertyName("location")]
        public string? Location { get; init; }

        [JsonPropertyName("salary")]
        public string? SalaryRange { get; init; }

        [JsonPropertyName("date")]
        public string? DatePosted { get; init; }
    }

    public record RemoteOkSearchRequest
    (
        string? Search = null,
        string? Tag = null
    );
}
