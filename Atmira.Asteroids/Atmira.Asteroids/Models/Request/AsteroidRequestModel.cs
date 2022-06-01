using System.ComponentModel.DataAnnotations;

namespace Atmira.Asteroids.Api.Models.Request;

public record AsteroidRequestModel
{
    [Required]
    public string Planet { get; init; } = default!;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public int ItemsPage { get; init; } = 10;

    public int Page { get; init; } = 1;
}
