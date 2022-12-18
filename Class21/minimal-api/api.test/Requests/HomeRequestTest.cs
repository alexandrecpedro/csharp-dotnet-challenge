using System.Net;
using System.Text.Json;
using Api.Test.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi;

namespace api.test.Requests;

[TestClass]
public class HomeRequestTest
{
    /* METHODS */
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
    public async Task TestaSeAHomeDaAPIExiste()
    {
        var response = await Setup.client.GetAsync("/");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("""{"mensagem":"Bem-vindo à API"}""", result);
    }

    [TestMethod]
    public async Task TestandoCaminhoFelizParaRecebeParametro()
    {
        var response = await Setup.client.GetAsync("/recebe-parametro?nome=Leandro");

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("""{"parametroPassado":"Alterando parametro recebido Leandro","mensagem":"Muito bem alunos! Passamos um parâmetro por query string"}""", result);
    }

    [TestMethod]
    public async Task TestandoRecebeParametroSemOParametro()
    {
        var response = await Setup.client.GetAsync("/recebe-parametro");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("""{"mensagem":"Olha, você não mandou uma informação importante. O nome é obrigatório!"}""", result);
    }
}