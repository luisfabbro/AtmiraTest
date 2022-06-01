using Newtonsoft.Json;

namespace Atmira.Asteroids.Core.Models;

public record CloseApproachDataModel
{
    [JsonProperty("orbiting_body")]
    public string OrbitingBody { get; init; } = default!;

    [JsonProperty("relative_velocity")]
    public RelativeVelocity RelativeVelocity { get; init; } = default!;

    [JsonProperty("close_approach_date")]
    public DateTime CloseAproachDate { get; init; }
}
