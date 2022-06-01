using Atmira.Asteroids.Api.Models.Request;
using Atmira.Asteroids.Api.Models.Response;
using Atmira.Asteroids.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atmira.Asteroids.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AsteroidController : ControllerBase
{
    #region Constructor
    readonly INasaApi _nasa;

    public AsteroidController(INasaApi nasa)
    {
        _nasa = nasa;
    }
    #endregion

    [HttpGet("/asteroids")]
    async public Task<IActionResult> Index([FromQuery]AsteroidRequestModel model)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }

        var asteroids = await _nasa.ListAsteroids(model.StartDate, model.EndDate);

        var data = asteroids.Where(x => x.CloseApproachData.Select(x => x.OrbitingBody).Contains(model.Planet))
            .Select(x => new AsteroidResponseModel
            {
                Name = x.Name,
                Planet = x.CloseApproachData.First().OrbitingBody,
                Diameter = (x.EstimatedDiameter.Kilometers.MinDiameter + x.EstimatedDiameter.Kilometers.MinDiameter) / 2,
                Velocity = x.CloseApproachData.First().RelativeVelocity.KilometersPerHour,
                Date = x.CloseApproachData.First().CloseAproachDate
            })
            .Skip(model.Page-1)
            .Take(model.ItemsPage)            
            .ToList();

        var result = new
        {
            data,
            model.ItemsPage,
            model.Page,
            TotalRecords = asteroids.Count
        };

        return Ok(result);
    }
}
