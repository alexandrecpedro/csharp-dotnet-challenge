using Microsoft.EntityFrameworkCore;
using MinimalApi;
using MinimalApi.Models;

namespace MinimalApi.Infrastructure.Database;

public class DbContexto : DbContext
{
    // ATTRIBUTES
    public virtual DbSet<Cliente> Clientes { get; set; } = default!;
    public virtual DbSet<Administrador> Administradores { get; set; } = default!;

    // CONSTRUCTOR
    public DbContexto() { }

    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

    // METHODS
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = Startup.StrConexao();
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }
} 