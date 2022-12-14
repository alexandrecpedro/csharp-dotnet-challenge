using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Infrastructure.Database;

public class DbContexto : DbContext
{ 
    public DbSet<Cliente> Clientes { get; set; } = default!;

    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }
} 