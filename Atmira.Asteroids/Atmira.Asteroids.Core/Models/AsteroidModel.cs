using Newtonsoft.Json;

namespace Atmira.Asteroids.Core.Models;

public record AsteroidModel
{
    [JsonProperty("name")]
    public string Name { get; init; } = default!;

    [JsonProperty("is_potentially_hazardous_asteroid")]    
    public bool IsPotentiallyHazardous { get; init; }

    [JsonProperty("estimated_diameter")]
    public EstimatedDiameterModel EstimatedDiameter { get; init; } = default!;

    [JsonProperty("close_approach_data")]    
    public CloseApproachDataModel[] CloseApproachData { get; init; } = default!;
}