using System.Net;
using System.Text;
using System.Text.Json;
using Api.Test.Helpers;
using MinimalApi.DTOs;

namespace api.test.Requests;

[TestClass]
public class AdministradoresRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task GetClientes()
    {
        var response = await Setup.client.GetAsync("/administradores");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task Login()
    {
        var loginDTO = new LoginDTO()
        {
            Email = "danilo@torneseumprogramador.com.br",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
        var response = await Setup.client.PostAsync("/login", content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogadoDTO>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado);
        Assert.IsNotNull(admLogado.Token);
    }
}