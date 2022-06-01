using System.Text.Json.Serialization;

namespace Atmira.Asteroids.Web.Models.ApiResponse;

public record AsteroidApiResponseModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("diameter")]
    public double Diameter { get; init; }

    [JsonPropertyName("velocity")]
    public double Velocity { get; init; }

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("planet")]
    public string Planet { get; init; } = default!;
}
