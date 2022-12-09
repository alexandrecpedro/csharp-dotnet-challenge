using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;

namespace Programa.Test.Models;

[TestClass]
public class ContaCorrenteTest
{
    // METHODS
    [TestMethod]
    public void TestandoPropriedadesDaClasse() 
    {
        var contaCorrenteTest = new ContaCorrente() { 
            ID = Guid.NewGuid().ToString(),
            IdCliente = "234567" 
        };
        contaCorrenteTest.Valor = 1;
        contaCorrenteTest.Data = DateTime.Now;

        // Testing Getter (if it is private)
        Assert.AreEqual("234567", contaCorrenteTest.IdCliente);
        Assert.AreEqual(1, contaCorrenteTest.Valor);
        Assert.IsNotNull(contaCorrenteTest.Data);
    }
}