using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi;

namespace api.test.Request;

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

        _http = _http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", PORT).UseEnvironment("Testing");
        });
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _http.Dispose();
    }

    [TestMethod]
    public async Task TestaSeAHomeDaAPIExiste()
    {
        var client = _http.CreateClient();
        var response = await client.GetAsync("/");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        // Assert.AreEqual("application/json; charset=utf-8");
    }
}