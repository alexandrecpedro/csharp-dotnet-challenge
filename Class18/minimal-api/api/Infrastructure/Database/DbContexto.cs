using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Infrastructure.Database;

public class DbContexto : DbContext
{ 
    public DbSet<Cliente> Clientes { get; set; } = default!;

    public DbContexto() { }

    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = Startup.StrConexao();
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }
} 