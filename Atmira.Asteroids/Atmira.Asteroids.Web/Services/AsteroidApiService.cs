using Atmira.Asteroids.Web.Models.ApiRequest;
using Atmira.Asteroids.Web.Models.ApiResponse;
using System.Text.Json;

namespace Atmira.Asteroids.Web.Services;

public interface IAsteroidApiService
{
    Task<List<AsteroidApiResponseModel>?> ListAsteroids(AsteroidApiRequestModel model);
}

public class AsteroidApiService : IAsteroidApiService
{
    #region Constructor
    readonly string AsteroidEndpoint;
    public AsteroidApiService(IConfiguration configuration)
    {
        AsteroidEndpoint = $"{configuration["Api:Url"]}/asteroids";
    }
    #endregion

    async public Task<List<AsteroidApiResponseModel>?> ListAsteroids(AsteroidApiRequestModel model)
    {
        var client = new HttpClient();
        var url = $"{AsteroidEndpoint}?planet={model.Planet}&startDate={model.StartDate:yyyy-MM-dd}&endDate={model.EndDate:yyyy-MM-dd}";

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<AsteroidApiResponseModel>>(content);
        return result;
    }
}
