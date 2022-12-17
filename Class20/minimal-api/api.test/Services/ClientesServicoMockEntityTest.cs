using System.Threading.Tasks;
using Api.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Models;
using MinimalApi.Services;
using Moq;

namespace Api.Test.Services;

[TestClass]
public class ClientesServicoMockEntityTest
{
    [TestMethod]
    public async Task TestaSalvar()
    {
        var mockContext = new Mock<DbContexto>();
        var dbsetClientes = new Mock<DbSet<Cliente>>();
        mockContext.Setup(c => c.Clientes).Returns(dbsetClientes.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        var clientesServico = new ClientesServico(mockContext.Object);

        var cliente = new Cliente()
        {
            Nome = "Usuario Teste",
            Telefone = "111111",
            Email = "Usuario@Teste.com",
        };

       await clientesServico.Salvar(cliente);
    }

    [TestMethod]
    public async Task TestaTodos()
    {
        var lista = new List<Cliente>
        {
            new Cliente { Id = 1, Nome = "BBB" },
            new Cliente { Id = 2, Nome = "ZZZ" },
            new Cliente { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();

        var mockContext = new Mock<DbContexto>();
        var mockSet = new Mock<DbSet<Cliente>>();
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        mockContext.Setup(c => c.Clientes).Returns(mockSet.Object);

        var clientesServico = new ClientesServico(mockContext.Object);

        var listaRetorno = await clientesServico.Todos();

        Assert.AreEqual(3, listaRetorno.Count);
    }

    [TestMethod]
    public async Task TestaBuscaPorId()
    {
        var lista = new List<Cliente>
        {
            new Cliente { Id = 1, Nome = "BBB" },
            new Cliente { Id = 2, Nome = "ZZZ" },
            new Cliente { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();

        var mockContext = new Mock<DbContexto>();
        var mockSet = new Mock<DbSet<Cliente>>();
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        mockContext.Setup(c => c.Clientes).Returns(mockSet.Object);

        var clientesServico = new ClientesServico(mockContext.Object);

        var cliente = await clientesServico.BuscaPorId(1);

        Assert.IsNotNull(cliente);
    }
}