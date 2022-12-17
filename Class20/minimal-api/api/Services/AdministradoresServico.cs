using Microsoft.EntityFrameworkCore;
using MinimalApi.Infrastructure.Database;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;

namespace MinimalApi.Services;

public class AdministradoresServico : ILogin<Administrador>
{
    // ATTRIBUTES
    private DbContexto dbContexto = default!;

    // CONSTRUCTOR
    public AdministradoresServico() {}

    public AdministradoresServico(DbContexto dbContexto)
    {
        this.dbContexto = dbContexto;
    }

    // METHODS
    public virtual async Task<Administrador?> LoginAsync(string email, string senha)
    {
        return await Task.FromResult(
            this.dbContexto.Administradores
                .Where(a => a.Email == email && a.Senha == senha)
                .FirstOrDefault()
        );
    }

    public virtual async Task Salvar(Administrador administrador)
    {
        if(administrador.Id == 0)
            this.dbContexto.Administradores.Add(administrador);
        else
            this.dbContexto.Administradores.Update(administrador);

        var ret = this.dbContexto.SaveChanges();
        if(ret != 1) throw new Exception("Não foi possivel salvar o dado no banco");
        await Task.FromResult(ret);
    }

    public async Task Update(Administrador administradorPara, object administradorDe)
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

        this.dbContexto.Administradores.Update(administradorPara);
        await this.dbContexto.SaveChangesAsync();
    }

    public async Task ExcluirPorId(int id)
    {
        var administrador = await this.dbContexto.Administradores.Where(c => c.Id == id).FirstAsync();
        if(administrador is not null)
        {
            await Excluir(administrador);
        }
    }

    public async Task Excluir(Administrador administrador)
    {
        this.dbContexto.Administradores.Remove(administrador);
        await this.dbContexto.SaveChangesAsync();
    }

    public async Task<Administrador?> BuscaPorId(int id)
    {
        Administrador? administrador = await Task.FromResult(this.dbContexto.Administradores.Where(c => c.Id == id).FirstOrDefault());
        return administrador;
    }

    public async Task<List<Administrador>> Todos()
    {
        return await Task.FromResult(this.dbContexto.Administradores.ToList());
    }
}