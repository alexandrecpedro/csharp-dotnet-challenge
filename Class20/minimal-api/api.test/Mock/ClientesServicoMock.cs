using System.Threading.Tasks;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;

namespace Api.Test.Mock;

public class ClientesServicoMock : IBancoDeDadosServico<Cliente>
{
    // ATTRIBUTES

    // CONSTRUCTOR

    // METHODS
    public Task<Cliente?> BuscaPorId(int id)
    {
        Cliente? cliente = null;

        if(id != 1) return Task.FromResult(cliente);

        cliente = new Cliente() 
        {
            Id = 1,
            Nome = "UsuarioMock",
            Telefone = "(11) 11111-1111",
            Email = "teste@mock.com"
        };

        return Task.FromResult(cliente ?? null);
    }

    public Task Excluir(Cliente objeto)
    {
        return Task.FromResult(()=>{});
    }

    public Task ExcluirPorId(int id)
    {
        return Task.FromResult(()=>{});
    }

    public Task Salvar(Cliente objeto)
    {
        objeto.Id = 1;
        return Task.FromResult(()=>{});
    }

    public Task Update(Cliente clientePara, object clienteDe)
    {
        if(clientePara.Id == 0)
            throw new Exception("Id de cliente é obrigatório");

        foreach(var propDe in clienteDe.GetType().GetProperties())
        {
            var propPara = clientePara.GetType().GetProperty(propDe.Name);
            if(propPara is not null)
            {
                propPara.SetValue(clientePara, propDe.GetValue(clienteDe));
            }
        }

        return Task.FromResult(()=>{});
    }

    public Task<List<Cliente>> Todos()
    {
        var lista = new List<Cliente>()
        {
            new Cliente() 
            {
                Id = 1,
                Nome = "UsuarioMock",
                Telefone = "(11) 11111-1111",
                Email = "teste@mock.com"
            }
        };

        return Task.FromResult(lista);
    }
}