using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi;

namespace api_test.Request;

[TestClass]
public class HomeRequestTest
{
    public const string PORT = "5001";

    private static TestContext _testContext = default!;
    private static WebApplicationFactory<Startup> _http = default!;

    /* METHODS */
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        _testContext = testContext;
        _http = new WebApplicationFactory<Startup>();
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _http.Dispose();
    }

    [TestMethod]
    public async Task TestaSeAHomeDaAPIExiste()
    {
        _http = _http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", PORT).UseEnvironment("Testing");
        });

        var client = _http.CreateClient();
        var response = await client.GetAsync("/");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}