using Microsoft.EntityFrameworkCore;
using MinimalApi;
using MinimalApi.Models;

namespace MinimalApi.Infrastructure.Database;

public class DbContextoTeste : DbContext
{
    public DbSet<Cliente> Clientes { get; set; } = default!;

    public DbContextoTeste() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = Startup.StrConexao();
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }
} 