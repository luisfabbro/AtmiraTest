using Newtonsoft.Json;

namespace Atmira.Asteroids.Core.Models;

public record KilometersModel
{
    [JsonProperty("estimated_diameter_min")]
    public double MinDiameter { get; init; }

    [JsonProperty("estimated_diameter_max")]
    public double MaxDiameter { get; init; }
}
