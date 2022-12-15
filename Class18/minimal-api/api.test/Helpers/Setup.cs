using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MinimalApi;
using MinimalApi.Infrastructure.Database;

namespace Api.Test.Helpers;

public class Setup
{
    // ATTRIBUTES
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    // TEST METHODS
    public static async Task ExecutaComandoSql(string sql)
    {
        await new DbContexto().Database.ExecuteSqlRawAsync(sql);
    }

    public static async Task<int> ExecutaEntityCount(int id, string nome)
    {
        return await new DbContexto().Clientes.Where(c => c.Id == id && c.Nome == nome).CountAsync();
    }

    public static async Task FakeCliente()
    {
        await new DbContexto().Database.ExecuteSqlRawAsync("""
        insert clientes(Nome, Telefone, Email, DataCriacao)
        values('Danilo', '(11) 11111-1111', 'email@teste.com', '2022-12-15 06:09:00')
        """);
    }

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<Startup>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");
            //builder.ConfigureServices.service.UseMySql // Caso queira deixar o teste com conexão diferente
        });

        Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
        Setup.http.Dispose();
    }
}