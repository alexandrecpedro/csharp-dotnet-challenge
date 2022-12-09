using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Infra;
using Programa.Models;
using Programa.Services;

namespace Programa.Test.Models;

[TestClass]
public class ContaCorrenteServicoTest
{
    // ATTRIBUTES
    private ContaCorrenteServico contaCorrenteServico = new ContaCorrenteServico(new JsonDriver<ContaCorrente>(Environment.GetEnvironmentVariable("RECORD_LOCAL_TEST_DOTNET7_CHALLENGE") ?? "/tmp"));

    // METHODS
    #region Metodos de setup test
    [TestInitialize()]
    public async Task Startup()
    {
        await contaCorrenteServico.Persistence.DeleteAll();
    }

    [TestCleanup()]
    public void Cleanup()
    {
        Console.WriteLine("=========[Depois do teste]==========");
    }
    #endregion

    #region Metodos helpers
    private async Task criaDadosContaFake(string idCliente, double[] valores)
    {
        foreach (var valor in valores)
        {
            await contaCorrenteServico.Persistence.Save(new ContaCorrente() {
                ID = Guid.NewGuid().ToString(),
                IdCliente = idCliente,
                Valor = valor,
                Data = DateTime.Now
            });
        }
    }
    #endregion

    [TestMethod]
    public void TestandoInjecaoDeDependencia()
    {
        var contaCorrenteServicoJson = new ContaCorrenteServico(new JsonDriver<ContaCorrente>("bla"));
        Assert.IsNotNull(contaCorrenteServicoJson);
        Assert.IsNotNull(contaCorrenteServicoJson.Persistence);

        var contaCorrenteServicoCsv = new ContaCorrenteServico(new CsvDriver<ContaCorrente>("bla"));
        Assert.IsNotNull(contaCorrenteServicoCsv);
        Assert.IsNotNull(contaCorrenteServicoCsv.Persistence);
    }

    [TestMethod]
    public async Task TestandoRetornoDoExtrato()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        await criaDadosContaFake(idCliente, new double[] { 100.5, 10 });

        // Processamento dados (Act)
        var extrato = await contaCorrenteServico.ExtratoCliente(idCliente);

        // Validação (Assert)
        Assert.AreEqual(2, extrato.Count);
    }

    [TestMethod]
    public async Task TestandoRetornoDoExtratoComQuantidadeAMais()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        await criaDadosContaFake(idCliente, new double[] { 100.01, 50 });

        var idCliente2 = Guid.NewGuid().ToString();
        await criaDadosContaFake(idCliente2, new double[] { 40 });

        // Processamento dados (Act)
        var extrato = await contaCorrenteServico.ExtratoCliente(idCliente2);

        // Validação (Assert)
        Assert.AreEqual(1, extrato.Count);
        Assert.AreEqual(3, (await contaCorrenteServico.Persistence.FindAll()).Count);
    }

    [TestMethod]
    public async Task TestandoSaldoDeUmCliente()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        await criaDadosContaFake(idCliente, new double[] { 5, 5, 5, -10 });
        await criaDadosContaFake(Guid.NewGuid().ToString(), new double[] { 300, 45 });

        // Processamento dados (Act)
        var saldo = await contaCorrenteServico.SaldoCliente(idCliente);

        // Validação (Assert)
        Assert.AreEqual(5, saldo);
        Assert.AreEqual(6, (await contaCorrenteServico.Persistence.FindAll()).Count);
    }
}
