using Newtonsoft.Json;

namespace Atmira.Asteroids.Core.Models;

public record EstimatedDiameterModel
{
    [JsonProperty("kilometers")]
    public KilometersModel Kilometers { get; init; } = default!;
}
