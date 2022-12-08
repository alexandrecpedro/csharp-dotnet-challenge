using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Services;

namespace Programa.Test.Models;

[TestClass]
public class ContaCorrenteServicoTest
{
    // METHODS
    #region Metodos de setup test
    [TestInitialize()]
    public void Startup()
    {
        // Console.WriteLine("=========[Antes do teste]==========");
        ContaCorrenteServico.Get().Lista = new List<ContaCorrente>();
    }

    [TestCleanup()]
    public void Cleanup()
    {
        // ContaCorrenteServico.Get().Lista = new List<ContaCorrente>();
        Console.WriteLine("=========[Depois do teste]==========");
    }
    #endregion

    #region Metodos helpers
    private void criaDadosContaFake(string idCliente, double[] valores)
    {
        foreach (var valor in valores)
        {
            ContaCorrenteServico.Get().Lista.Add(new ContaCorrente() {
                ID = Guid.NewGuid().ToString(),
                IdCliente = idCliente,
                Valor = valor,
                Data = DateTime.Now
            });
        }
    }
    #endregion

    [TestMethod]
    public void TestandoUnicaInstanciaDoServico()
    {
        Assert.IsNotNull(ContaCorrenteServico.Get());
        Assert.IsNotNull(ContaCorrenteServico.Get().Lista);

        ContaCorrenteServico.Get().Lista.Add(new ContaCorrente() { 
            ID = Guid.NewGuid().ToString(),
            IdCliente = "2122222" 
        });

        Assert.AreEqual(1, ContaCorrenteServico.Get().Lista.Count);
    }

    [TestMethod]
    public void TestandoRetornoDoExtrato()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        criaDadosContaFake(idCliente, new double[] { 100.5, 10 });

        // Processamento dados (Act)
        var extrato = ContaCorrenteServico.Get().ExtratoCliente(idCliente);

        // Validação (Assert)
        Assert.AreEqual(2, extrato.Count);
    }

    [TestMethod]
    public void TestandoRetornoDoExtratoComQuantidadeAMais()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        criaDadosContaFake(idCliente, new double[] { 100.01, 50 });

        var idCliente2 = Guid.NewGuid().ToString();
        criaDadosContaFake(idCliente2, new double[] { 40 });

        // Processamento dados (Act)
        var extrato = ContaCorrenteServico.Get().ExtratoCliente(idCliente2);

        // Validação (Assert)
        Assert.AreEqual(1, extrato.Count);
        Assert.AreEqual(3, ContaCorrenteServico.Get().Lista.Count);
    }

    [TestMethod]
    public void TestandoSaldoDeUmCliente()
    {
        // Preparação (Arrange)
        var idCliente = Guid.NewGuid().ToString();
        criaDadosContaFake(idCliente, new double[] { 5, 5, 5, -10 });
        criaDadosContaFake(Guid.NewGuid().ToString(), new double[] { 300, 45 });

        // Processamento dados (Act)
        var saldo = ContaCorrenteServico.Get().SaldoCliente(idCliente);

        // Validação (Assert)
        Assert.AreEqual(5, saldo);
        Assert.AreEqual(6, ContaCorrenteServico.Get().Lista.Count);
    }
}
