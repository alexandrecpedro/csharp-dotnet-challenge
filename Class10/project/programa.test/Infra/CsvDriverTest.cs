using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Infra.Interfaces;
using Programa.Models;

namespace Programa.Infra;

[TestClass]
public class CsvDriverTest
{
    // ATTRIBUTES
    private string testFilePath;
    private CsvDriver csvDriver;

    // CONSTRUCTOR
    public CsvDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE") ?? "/tmp";
        this.testFilePath = path;
        this.csvDriver = new CsvDriver(this.testFilePath);
    }

    // TEST METHODS
    [TestMethod]
    public async Task TestandoDriverCsvParaClientes()
    {

        var cliente = new Cliente(){
            ID = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await this.csvDriver.Save(cliente);

        var existe = File.Exists(this.testFilePath + "/clientes.csv");
    }

    [TestMethod]
    public async Task TestandoDriverCsvParaContaCorrente()
    {
        var contaCorrente = new ContaCorrente(){
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await csvDriver.Save(contaCorrente);

        var existe = File.Exists(this.testFilePath + "/contacorrentes.csv");
    }
}