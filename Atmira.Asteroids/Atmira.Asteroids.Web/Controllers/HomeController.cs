using Atmira.Asteroids.Web.Models.ApiRequest;
using Atmira.Asteroids.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atmira.Asteroids.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor
        readonly IAsteroidApiService _apiService;

        public HomeController(IAsteroidApiService apiService)
        {
            _apiService = apiService;
        }
        #endregion

        async public Task<IActionResult> Index()
        {
            var model = new AsteroidApiRequestModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Planet = "Earth"
            };

            var asteroids = await _apiService.ListAsteroids(model);
            ViewData["Asteroids"] = asteroids;

            return View(model);
        }

        [HttpPost]
        async public Task<IActionResult> Index(AsteroidApiRequestModel model)
        {
            if (ModelState.IsValid is false)
            {
                return View("Index", model);
            }

            var asteroids = await _apiService.ListAsteroids(model);
            ViewData["Asteroids"] = asteroids;

            return View("Index", model);
        }
    }
}