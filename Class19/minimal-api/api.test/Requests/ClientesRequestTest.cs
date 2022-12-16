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
    }

    [TestMethod]
    public async Task PutClientesSemNome()
    {
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
        var response = await Setup.client.DeleteAsync($"/clientes/{1}");
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [TestMethod]
    public async Task DeleteClientesIdNaoExistente()
    {
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
        var response = await Setup.client.GetAsync($"/clientes/{1}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}