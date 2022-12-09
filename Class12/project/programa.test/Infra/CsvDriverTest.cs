using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Infra;

namespace Programa.Test.Infra;

[TestClass]
public class CsvDriverTest
{
    // ATTRIBUTES
    private string testFilePath;

    // CONSTRUCTOR
    public CsvDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE") ?? "/tmp";
        this.testFilePath = path;
    }

    // TEST METHODS
    [TestMethod]
    public async Task TestandoDriverCsvParaClientes()
    {
        var csvDriver = new CsvDriver<Cliente>(this.testFilePath);

        var cliente = new Cliente(){
            ID = Guid.NewGuid().ToString(),
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11) 9999-9999"
        };

        await csvDriver.Save(cliente);

        var existe = File.Exists(this.testFilePath + "/clientes.csv");
    }

    [TestMethod]
    public async Task TestandoDriverCsvParaContaCorrente()
    {
        var csvDriver = new CsvDriver<ContaCorrente>(this.testFilePath);
        
        var contaCorrente = new ContaCorrente(){
            ID = Guid.NewGuid().ToString(),
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 200,
            Data = DateTime.Now
        };

        await csvDriver.Save(contaCorrente);

        var existe = File.Exists(this.testFilePath + "/contacorrentes.csv");
    }
}