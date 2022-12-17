using System.Threading.Tasks;
using Api.Test.Helpers;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Models;
using MinimalApi.Services;

namespace Api.Test.Services;

[TestClass]
public class ClientesServicoTest
{
    [ClassInitialize]
    public static async Task ClassInit(TestContext testContext)
    {
        await Setup.ExecutaComandoSqlAsync("truncate table clientes");
    }

    [ClassCleanup]
    public static async Task ClassCleanup()
    {
        await Setup.ExecutaComandoSqlAsync("truncate table clientes");
    }

    [TestMethod]
    public async Task TestaSalvarDadoNobanco()
    {
        var clientesServico = new ClientesServico(new DbContexto());

        var cliente = new Cliente()
        {
            Nome = "Usuario Teste",
            Telefone = "111111",
            Email = "Usuario@Teste.com",
        };

        await clientesServico.Salvar(cliente);
        
        Assert.AreEqual(1, cliente.Id);
    }
}