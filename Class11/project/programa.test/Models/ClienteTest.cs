using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;

namespace Programa.Test.Models;

[TestClass]
public class ClienteTest
{
    // METHODS
    [TestMethod]
    public void TestandoPropriedadesDaClasse() 
    {
        var cliente = new Cliente() { ID = "234567" };
        cliente.Nome = "Marcela";
        cliente.Telefone = "(11) 11111-1111";
        cliente.Email = "ma@teste.com";

        // Testing Getter (if it is private)
        Assert.AreEqual("234567", cliente.ID);
        Assert.AreEqual("Marcela", cliente.Nome);
        Assert.AreEqual("(11) 11111-1111", cliente.Telefone);
        Assert.AreEqual("ma@teste.com", cliente.Email);
    }
}