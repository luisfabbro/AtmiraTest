using Atmira.Asteroids.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Atmira.Asteroids.Web.Models.ApiRequest;

public record AsteroidApiRequestModel
{
    [Required]
    public string Planet { get; init; } = default!;
        
    public DateTime StartDate { get; init; }

    [DateGreaterThan("StartDate")]
    public DateTime EndDate { get; init; }
}
