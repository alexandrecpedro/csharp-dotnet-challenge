using System.Threading.Tasks;
using Api.Test.Helpers;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Models;
using MinimalApi.Services;
using Moq;

namespace Api.Test.Services;

[TestClass]
public class ClientesServicoMockTest
{
    [TestMethod]
    public async Task TestaSalvarDadoNobanco()
    {
        var mockClientesServico = new Mock<ClientesServico>();

        var cliente = new Cliente()
        {
            Nome = "Usuario Teste",
            Telefone = "111111",
            Email = "Usuario@Teste.com",
        };

        var clienteMock = cliente;
        clienteMock.Id = 1;

        mockClientesServico.Setup(s => s.Salvar(cliente)).Returns(Task.FromResult(clienteMock));
        await mockClientesServico.Object.Salvar(cliente);
        
        Assert.AreEqual(1, cliente.Id);
    }
}