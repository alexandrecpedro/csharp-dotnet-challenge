using Microsoft.VisualStudio.TestTools.UnitTesting;
using Programa.Models;
using Programa.Services;

namespace Programa.Test.Models;

[TestClass]
public class ClienteServicoTest
{
    // METHODS
    [TestMethod]
    public void TestandoUnicaInstanciaDoServico() 
    {
        // Testing Getter (if it is private)
        Assert.IsNotNull(ClienteServico.Get());
        Assert.IsNotNull(ClienteServico.Get().Lista);

        ClienteServico.Get().Lista.Add(new Cliente(){
            ID = "23421",
            Nome = "teste"
        });

        Assert.AreEqual(1, ClienteServico.Get().Lista.Count());
    }
}