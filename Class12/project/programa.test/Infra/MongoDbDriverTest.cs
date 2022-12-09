using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Infra;
using Programa.Test.Infra.Entidades;
using MongoDB.Bson;

namespace Programa.Test.Infra;

[TestClass]
public class MongoDbDriverTest
{
    // ATTRIBUTES
    private string testFilePath;

    // CONSTRUCTOR
    public MongoDbDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE_MONGODB") ?? "mongodb://localhost#desafio21dias_dotnet7";
        this.testFilePath = path;
    }

    // TEST METHODS
    [TestInitialize()]
    public async Task Startup()
    {
        await new MongoDbDriver<ClienteMongoDb>(this.testFilePath).DeleteAll();
        await new MongoDbDriver<ContaCorrenteMongoDb>(this.testFilePath).DeleteAll();
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaClientes()
    {
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.testFilePath);

        var cliente = new ClienteMongoDb(){
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11) 9999-9999"
        };

        await jsonDriver.Save(cliente);

        var existe = File.Exists(this.testFilePath + "/clientes.json");
    }

    [TestMethod]
    public async Task TestandoDriverJsonParaContaCorrente()
    {
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.testFilePath);
        
        var contaCorrente = new ContaCorrenteMongoDb(){
            IdCliente = ObjectId.GenerateNewId(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Save(contaCorrente);

        var existe = File.Exists(this.testFilePath + "/contacorrentes.json");
    }

    [TestMethod]
    public async Task TestandoBuscaDeTodasAsEntidades()
    {
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.testFilePath);

        var contaCorrente = new ContaCorrenteMongoDb() {
            IdCliente = ObjectId.GenerateNewId(),
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
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.testFilePath);

        var cliente = new ClienteMongoDb() {
            Nome = "Danilo " + DateTime.Now,
            Email = "danilo@teste.com",
            Telefone = "(11)9999-9999"
        };

        await jsonDriver.Save(cliente);

        var clienteDb = await jsonDriver.FindById(cliente.ID.ToString());

        Assert.AreEqual(cliente.Nome, clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoAlteracaoDeEntidade()
    {
        var jsonDriver = new MongoDbDriver<ClienteMongoDb>(this.testFilePath);

        var cliente = new ClienteMongoDb() {
            Nome = "Danilo",
            Email = "danilo@teste.com",
            Telefone = "(11) 9999-9999"
        };

        await jsonDriver.Save(cliente);

        cliente.Nome = "Danilo Santos";

        await jsonDriver.Save(cliente);

        var clienteDb = await jsonDriver.FindById(cliente.ID.ToString());

        Assert.AreEqual("Danilo Santos", clienteDb?.Nome);
    }

    [TestMethod]
    public async Task TestandoExcluirEntidade()
    {
        var jsonDriver = new MongoDbDriver<ContaCorrenteMongoDb>(this.testFilePath);

        var contaCorrente = new ContaCorrenteMongoDb() {
            IdCliente = ObjectId.GenerateNewId(),
            Valor = 200,
            Data = DateTime.Now
        };

        await jsonDriver.Save(contaCorrente);

        var objDb = await jsonDriver.FindById(contaCorrente.ID.ToString());
        Assert.IsNotNull(objDb);
        Assert.IsNotNull(objDb?.ID);

        await jsonDriver.Delete(contaCorrente);

        var objDb2 = await jsonDriver.FindById(contaCorrente.ID.ToString());
        Assert.IsNull(objDb2);
    }
}