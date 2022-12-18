using System.Threading.Tasks;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;

namespace Api.Test.Mock;

class AdministradoresServicoMock : ILogin<Administrador>
{
    public static Administrador AdministradorFake()
    {
        return new Administrador
            {
                Nome = "Danilo",
                Email = "danilo@torneseumprogramador.com.br",
                Senha = "123456",
                Permissao = "administrador"
            };
    }

    public Task<Administrador?> BuscaPorId(int id)
    {
        Administrador? administrador = null;

        if(id != 1) return Task.FromResult(administrador);

        administrador = new Administrador() 
        {
            Id = 1,
            Nome = "UsuarioMock",
            Permissao = "administrador",
            Email = "teste@mock.com"
        };

        return Task.FromResult(administrador ?? null);
    }

    public Task Excluir(Administrador objeto)
    {
        return Task.FromResult(()=>{});
    }

    public Task ExcluirPorId(int id)
    {
        return Task.FromResult(()=>{});
    }

    public Task Salvar(Administrador objeto)
    {
        objeto.Id = 1;
        return Task.FromResult(()=>{});
    }

    public Task Update(Administrador administradorPara, object administradorDe)
    {
        if(administradorPara.Id == 0)
            throw new Exception("Id de administrador é obrigatório");

        foreach(var propDe in administradorDe.GetType().GetProperties())
        {
            var propPara = administradorPara.GetType().GetProperty(propDe.Name);
            if(propPara is not null)
            {
                propPara.SetValue(administradorPara, propDe.GetValue(administradorDe));
            }
        }

        return Task.FromResult(()=>{});
    }

    public Task<List<Administrador>> Todos()
    {
        var lista = new List<Administrador>()
        {
            new Administrador() 
            {
                Id = 1,
                Nome = "UsuarioMock",
                Permissao = "administrador",
                Email = "teste@mock.com"
            }
        };

        return Task.FromResult(lista);
    }

    public Task<Administrador?> LoginAsync(string email, string senha)
    {
        return Task.FromResult(
            AdministradorFake() ?? null
        );
    }
}