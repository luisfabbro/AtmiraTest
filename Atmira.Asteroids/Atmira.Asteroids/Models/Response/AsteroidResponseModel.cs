namespace Atmira.Asteroids.Api.Models.Response;

public record AsteroidResponseModel
{
    public string Name { get; init; } = default!;
    public double Diameter { get; init; }
    public double Velocity { get; init; }
    public DateTime Date { get; init; }
    public string Planet { get; init; } = default!;
}
