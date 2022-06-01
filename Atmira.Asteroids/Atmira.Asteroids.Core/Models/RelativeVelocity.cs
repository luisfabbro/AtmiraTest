using Newtonsoft.Json;

namespace Atmira.Asteroids.Core.Models;

public record RelativeVelocity
{
    [JsonProperty("kilometers_per_hour")]
    public double KilometersPerHour { get; init; }
}
