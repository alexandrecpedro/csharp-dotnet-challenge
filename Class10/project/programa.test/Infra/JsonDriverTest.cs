using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Infra.Interfaces;
using Programa.Models;

namespace Programa.Infra;

[TestClass]
public class JsonDriverTest
{
    // ATTRIBUTES
    private string testFilePath;
    private JsonDriver jsonDriver;

    // CONSTRUCTOR
    public JsonDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE") ?? "/tmp";
        this.testFilePath = path;
        this.jsonDriver = new JsonDriver(this.testFilePath);
    }

    // TEST METHODS
    [TestMethod]
    public async Task TestandoDriverJsonParaCliente()
    {
        var cliente = new Cliente() {
            ID = Guid.NewGuid().ToString(),
            Nome = "Alexandre",
            Email = "alex@teste.com",
            Telefone = "(11) 3435-9876"
        };

        await this.jsonDriver.Save(cliente);

        // Verify if path exists
        var exists = File.Exists(this.testFilePath + "/clientes.json");
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaContaCorrente()
    {
        var contaCorrente = new ContaCorrente() {
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await this.jsonDriver.Save(contaCorrente);

        // Verify if path exists
        var exists = File.Exists(this.testFilePath + "/contacorrentes.json");
    }
}