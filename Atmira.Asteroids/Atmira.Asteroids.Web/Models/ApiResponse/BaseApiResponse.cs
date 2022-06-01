using System.Text.Json.Serialization;

namespace Atmira.Asteroids.Web.Models.ApiResponse;

public record BaseApiResponse
{
    [JsonPropertyName("itemsPage")]
    public int ItemsPerPage { get; init; }

    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; init; }

    [JsonPropertyName("data")]
    public List<AsteroidApiResponseModel> Data { get; init; } = default!;
}
