using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;

namespace Programa.Infra;

[TestClass]
public class JsonDriverTest
{
    // ATTRIBUTES
    private string testFilePath;

    // CONSTRUCTOR
    public JsonDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE") ?? "/tmp";
        this.testFilePath = path;
    }

    // TEST METHODS
    #region Setup test methods
    [TestInitialize()]
    public async Task Startup()
    {
        await new JsonDriver<Cliente>(this.testFilePath).DeleteAll();
        await new JsonDriver<ContaCorrente>(this.testFilePath).DeleteAll();
    }
    #endregion

    [TestMethod]
    public async Task TestandoDriverJsonParaCliente()
    {
        var jsonDriver = new JsonDriver<Cliente>(this.testFilePath);

        var cliente = new Cliente() {
            ID = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11) 3435-9876"
        };

        await jsonDriver.Save(cliente);

        // Verify if path exists
        var exists = File.Exists(this.testFilePath + "/clientes.json");
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaContaCorrente()
    {
        var jsonDriver = new JsonDriver<ContaCorrente>(this.testFilePath);

        var contaCorrente = new ContaCorrente() {
            ID = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Save(contaCorrente);

        // Verify if path exists
        var exists = File.Exists(this.testFilePath + "/contacorrentes.json");
    }

    [TestMethod]
    public async Task TestandoBuscaDeTodasAsEntidades()
    {
        var jsonDriver = new JsonDriver<ContaCorrente>(this.testFilePath);

        var contaCorrente = new ContaCorrente() {
            ID = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Save(contaCorrente);

        var todasContasCorrentes = await jsonDriver.FindAll();

        // Verify if path exists
        Assert.IsTrue(todasContasCorrentes.Count > 0);
        var exists = File.Exists(this.testFilePath + "/contacorrentes.json");
    }

    [TestMethod]
    public async Task TestandoBuscaPorId()
    {
        var jsonDriver = new JsonDriver<Cliente>(this.testFilePath);

        var cliente = new Cliente() {
            ID = Guid.NewGuid().ToString(),
            Nome = "Danilo " + DateTime.Now,
            Email = "danilo@teste.com",
            Telefone = "(11) 3435-9876"
        };

        await jsonDriver.Save(cliente);

        var clienteDb = await jsonDriver.FindById(cliente.ID);

        Assert.AreEqual(cliente.Nome, clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoAlteracaoDeEntidade()
    {
        var jsonDriver = new JsonDriver<Cliente>(this.testFilePath);

        var cliente = new Cliente() {
            ID = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11) 3435-9876"
        };

        await jsonDriver.Save(cliente);

        cliente.Nome = "Danilo Santos";

        await jsonDriver.Save(cliente);

        var clienteDb = await jsonDriver.FindById(cliente.ID);

        Assert.AreEqual("Danilo Santos", clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoExcluirEntidade()
    {
        var jsonDriver = new JsonDriver<ContaCorrente>(this.testFilePath);

        var contaCorrente = new ContaCorrente() {
            ID = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Save(contaCorrente);

        var objDb = await jsonDriver.FindById(contaCorrente.ID);
        Assert.IsNotNull(objDb);
        Assert.IsNotNull(objDb?.ID);

        await jsonDriver.Delete(contaCorrente);

        var objDb2 = await jsonDriver.FindById(contaCorrente.ID);
        Assert.IsNull(objDb2);
    }
}