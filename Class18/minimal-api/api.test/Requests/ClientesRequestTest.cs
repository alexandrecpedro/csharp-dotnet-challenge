using System.Net;
using System.Text;
using System.Text.Json;
using Api.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalApi.Models;
using MinimalApi.Infrastructure.Database;

namespace api.test.Request;

[TestClass]
public class ClientesRequestTest
{
    /* METHODS */
    [ClassInitialize]
    public static async Task ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
        await Setup.ExecutaComandoSql("truncate table clientes");
    }

    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        Setup.ClassCleanup();
        await Setup.ExecutaComandoSql("truncate table clientes");
    }

    [TestMethod]
    public async Task GetClientes()
    {
        await Setup.FakeCliente();
        var response = await Setup.client.GetAsync("/clientes");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        var clientes = JsonSerializer.Deserialize<List<Cliente>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(clientes);
        Assert.IsTrue(clientes.Count > 0);
        Assert.IsNotNull(clientes[0].Id);
        Assert.IsNotNull(clientes[0].Nome);
        Assert.IsNotNull(clientes[0].Email);
        Assert.IsNotNull(clientes[0].Telefone);
        Assert.IsNotNull(clientes[0].DataCriacao);
    }

    [TestMethod]
    public async Task PostClientes()
    {
        await Setup.ExecutaComandoSql("truncate table clientes");
        var cliente = new ClienteDTO()
        {
            Nome = "Janaina",
            Email = "jan@gmail.com",
            Telefone = "(11) 11111-1111"
        };

        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");
        var response = await Setup.client.PostAsync("/clientes", content);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        var clienteResponse = JsonSerializer.Deserialize<Cliente>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(clienteResponse);
        Assert.AreEqual(1, clienteResponse.Id);
    }

    [TestMethod]
    public async Task PutClientes()
    {
        await Setup.ExecutaComandoSql("truncate table clientes");
        await Setup.FakeCliente();

        var qtdInicial = await Setup.ExecutaEntityCount(1, "Danilo");
        Assert.AreEqual(1, qtdInicial);

        var cliente = new ClienteDTO()
        {
            Nome = "Janaina",
            Email = "jan@gmail.com",
            Telefone = "(11) 11111-1111"
        };

        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");
        var response = await Setup.client.PutAsync($"/clientes/{1}", content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        var clienteResponse = JsonSerializer.Deserialize<Cliente>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(clienteResponse);
        Assert.AreEqual(1, clienteResponse.Id);
        Assert.AreEqual("Janaina", clienteResponse.Nome);

        var qtdFinal = await Setup.ExecutaEntityCount(1, "Janaina");
        Assert.AreEqual(1, qtdFinal);
    }

    [TestMethod]
    public async Task PutClientesSemNome()
    {
        await Setup.FakeCliente();

        var cliente = new ClienteDTO()
        {
            Email = "jan@gmail.com",
            Telefone = "(11) 11111-1111"
        };

        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json");
        var response = await Setup.client.PutAsync($"/clientes/{1}", content);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var result = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("""{"codigo":123432,"mensagem":"O Nome é obrigatório"}""", result);
    }

    [TestMethod]
    public async Task DeleteClientes()
    {
        await Setup.FakeCliente();
        var response = await Setup.client.DeleteAsync($"/clientes/{1}");
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [TestMethod]
    public async Task DeleteClientesIdNaoExistente()
    {
        await Setup.ExecutaComandoSql("truncate table clientes");
        await Setup.FakeCliente();
        var response = await Setup.client.DeleteAsync($"/clientes/{5}");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task GetPorIdClienteNaoEncontrado()
    {
        var response = await Setup.client.GetAsync($"/clientes/{4}");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task GetPorId()
    {
        await Setup.ExecutaComandoSql("truncate table clientes");
        await Setup.FakeCliente();
        var response = await Setup.client.GetAsync($"/clientes/{1}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    /*
    [TestMethod]
    public async Task PatchClientes()
    {
        var cliente = new ClienteNomeDTO()
        {
            Nome = "Jaziel",
        };
        Setup.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
        var content = new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json-patch+json");
        var response = await Setup.client.PatchAsync($"/clientes/{1}", content);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        var result = await response.Content.ReadAsStringAsync();
        Assert.AreEqual("""{"codigo":123,"mensagem":"O Nome é obrigatório"}""", result);
    }
    */
}