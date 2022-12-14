using Microsoft.EntityFrameworkCore;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;

namespace MinimalApi.Services;

public class ClientesServico : IBancoDeDadosServico<Cliente>
{
    // ATTRIBUTES
    private DbContexto dbContexto;

    // CONSTRUCTOR
    public ClientesServico(DbContexto dbContexto)
    {
        this.dbContexto = dbContexto;
    }

    // METHODS
    public async Task Salvar(Cliente cliente)
    {
        if(cliente.Id == 0)
            this.dbContexto.Clientes.Add(cliente);
        else
            this.dbContexto.Clientes.Update(cliente);

        await this.dbContexto.SaveChangesAsync();
    }

    public async Task ExcluirPorId(int id)
    {
        var cliente = await this.dbContexto.Clientes.Where(c => c.Id == id).FirstAsync();
        if(cliente is not null)
        {
            await Excluir(cliente);
        }
    }

    public async Task Excluir(Cliente cliente)
    {
        this.dbContexto.Clientes.Remove(cliente);
        await this.dbContexto.SaveChangesAsync();
    }

    public async Task<Cliente> BuscaPorId(int id)
    {
        return await this.dbContexto.Clientes.Where(c => c.Id == id).FirstAsync();
    }

    public async Task<List<Cliente>> Todos()
    {
        return await this.dbContexto.Clientes.ToListAsync();
    }
}