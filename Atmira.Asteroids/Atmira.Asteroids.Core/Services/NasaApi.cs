using Atmira.Asteroids.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text.Json;

namespace Atmira.Asteroids.Core.Services;

public interface INasaApi
{ 
    Task<List<AsteroidModel>> ListAsteroids(DateTime startDate, DateTime endDate);
}

public class NasaApi : INasaApi
{
    #region Constructor    
    readonly string _url;
    const string RootPath = "near_earth_objects";

    public NasaApi(IConfiguration configuration)
    {
        var url = configuration["Nasa:ApiUrl"];
        var key = configuration["Nasa:ApiKey"];

        _url = $"{url}?api_key={key}";
    }
    #endregion

    async public Task<List<AsteroidModel>> ListAsteroids(DateTime startDate, DateTime endDate)
    {
        var content = await AsteroidsResponse(startDate, endDate);
        var result = ListAsteroids(content);

        return result;
    }

    async Task<string> AsteroidsResponse(DateTime startDate, DateTime endDate)
    {
        var baseUrl = _url;

        var startFormattedDate = startDate.ToString("yyyy-MM-dd");
        baseUrl += $"&start_date={startFormattedDate}";

        var endFormattedDate = endDate.ToString("yyyy-MM-dd");
        baseUrl += $"&end_date={endFormattedDate}";

        var client = new HttpClient();
        var response = await client.GetAsync(baseUrl);
        var content = await response.Content.ReadAsStringAsync();

        return content;
    }

    static List<AsteroidModel> ListAsteroids(string content)
    {        
        var result = new List<AsteroidModel>();

        JObject nearObjects = JObject.Parse(content);
        if (nearObjects[RootPath] is null)
        {
            return result;
        }

        var items = nearObjects[RootPath]!.ToList();

        foreach (var item in items)
        {
            var asteroids = item.First;
            if (asteroids is null)
            {
                continue;
            }

            foreach (var asteroid in asteroids)
            {
                var model = JsonConvert.DeserializeObject<AsteroidModel>(asteroid.ToString());
                if (model is null)
                {
                    continue;
                }

                result.Add(model);
            }
        }

        return result;
    }
}
