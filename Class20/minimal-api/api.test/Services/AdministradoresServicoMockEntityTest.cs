using System.Threading.Tasks;
using Api.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Models;
using MinimalApi.Services;
using Moq;

namespace Api.Test.Services;

[TestClass]
public class AdministradoresServicoMockEntityTest
{
    [TestMethod]
    public async Task TestaSalvar()
    {
        var mockContext = new Mock<DbContexto>();
        var dbsetAdministradores = new Mock<DbSet<Administrador>>();
        mockContext.Setup(c => c.Administradores).Returns(dbsetAdministradores.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        var administradoresServico = new AdministradoresServico(mockContext.Object);

        var cliente = new Administrador()
        {
            Nome = "Usuario Teste",
            Email = "Usuario@Teste.com",
            Permissao = "administrador",
        };

       await administradoresServico.Salvar(cliente);
    }

    [TestMethod]
    public async Task TestaTodos()
    {
        var lista = new List<Administrador>
        {
            new Administrador { Id = 1, Nome = "BBB" },
            new Administrador { Id = 2, Nome = "ZZZ" },
            new Administrador { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();
        var administradoresServico = new AdministradoresServico(contextMock(data));

        var listaRetorno = await administradoresServico.Todos();

        Assert.AreEqual(3, listaRetorno.Count);
    }

    [TestMethod]
    public async Task TestaLogin()
    {
        var lista = new List<Administrador>
        {
            new Administrador { Id = 1, Nome = "Danilo", Email = "danilo@torneseumprogramador.com.br", Senha = "123456" },
            new Administrador { Id = 2, Nome = "ZZZ" },
            new Administrador { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();
        var administradoresServico = new AdministradoresServico(contextMock(data));

        var adm = await administradoresServico.LoginAsync("danilo@torneseumprogramador.com.br", "123456");

        Assert.IsNotNull(adm);
    }

    private DbContexto contextMock(IQueryable<Administrador> data)
    {
        var mockContext = new Mock<DbContexto>();
        var mockSet = new Mock<DbSet<Administrador>>();
        mockSet.As<IQueryable<Administrador>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Administrador>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Administrador>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Administrador>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        mockContext.Setup(c => c.Administradores).Returns(mockSet.Object);

        return mockContext.Object;
    }

    [TestMethod]
    public async Task TestaLoginInvalido()
    {
        var lista = new List<Administrador>
        {
            new Administrador { Id = 1, Nome = "Danilo", Email = "danilo@torneseumprogramador.com.br", Senha = "123456" },
            new Administrador { Id = 2, Nome = "ZZZ" },
            new Administrador { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();
        var administradoresServico = new AdministradoresServico(contextMock(data));

        var adm = await administradoresServico.LoginAsync("errado@torneseumprogramador.com.br", "123ss456");

        Assert.IsNull(adm);
    }

    [TestMethod]
    public async Task TestaBuscaPorId()
    {
        var lista = new List<Administrador>
        {
            new Administrador { Id = 1, Nome = "BBB" },
            new Administrador { Id = 2, Nome = "ZZZ" },
            new Administrador { Id = 3, Nome = "AAA" },
        };
        
        var data = lista.AsQueryable();
        var administradoresServico = new AdministradoresServico(contextMock(data));

        var cliente = await administradoresServico.BuscaPorId(1);

        Assert.IsNotNull(cliente);
    }
}