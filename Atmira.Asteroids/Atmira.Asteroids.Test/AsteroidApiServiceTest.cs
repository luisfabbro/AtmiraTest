using Atmira.Asteroids.Core.Services;
using Atmira.Asteroids.Web.Models.ApiRequest;
using Atmira.Asteroids.Web.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace Atmira.Asteroids.Test;

public class AsteroidApiServiceTest
{    
    readonly AsteroidApiService service;    

    public AsteroidApiServiceTest()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Api:Url", "http://localhost:5123" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        service = new AsteroidApiService(configuration);
    }

    [Fact]
    async public Task ApiDateFilterTest()
    {
        //Arrange
        var result = "{ " +
                        "\"data\": [ " +
                            "{ " +
                                "\"name\": \"162882 (2001 FD58)\", " +
                                "\"diameter\": 0.477040198, " +
                                "\"velocity\": 115776.7985857936, " +
                                "\"date\": \"2022-05-31T00:00:00\", " +
                                "\"planet\": \"Earth\" " +
                            "}, " +
                            "{ " +
                                "\"name\": \"(2001 CQ36)\", " +
                                "\"diameter\": 0.0840533402, " +
                                "\"velocity\": 25045.5569764827, " +
                                "\"date\": \"2022-05-31T00:00:00\", " +
                                "\"planet\": \"Earth\" " +
                            "}, " +
                            "{ " +
                                "\"name\": \"(2010 LM68)\", " +
                                "\"diameter\": 0.1822027706, " +
                                "\"velocity\": 28559.2675855672, " +
                                "\"date\": \"2022-06-05T00:00:00\", " +
                                "\"planet\": \"Earth\" " +
                            "} " +
                        "], " +
                        "\"itemsPage\": 10, " +
                        "\"page\": 1, " +
                        "\"totalRecords\": 41 " +
                    "}";

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
           .Protected()
           // Setup the PROTECTED method to mock
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           // prepare the expected response of the mocked http call
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(result),
           })
           .Verifiable();

        //Act              
        var model = new AsteroidApiRequestModel
        {
            StartDate = new DateTime(2022, 05, 31),
            EndDate = new DateTime(2022, 06, 01),
            Planet = "Earth"
        };

        var list = await service.ListAsteroids(model);

        //Assert
        Assert.NotNull(list);
        Assert.True(list!.Data.Count == 2);
    }

}