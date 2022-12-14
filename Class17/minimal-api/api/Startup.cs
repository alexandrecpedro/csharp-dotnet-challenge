using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApi.Infrastructure.Database;
using MinimalApi.DTOs;
using MinimalApi.Models;
using MinimalApi.ModelViews;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Services;

namespace MinimalApi;

public class Startup
{
    // ATTRIBUTES
    public IConfiguration Configuration { get; }

    // CONSTRUCTOR
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // METHODS
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
        });

        services.AddEndpointsApiExplorer();

        string? connection = Environment.GetEnvironmentVariable("DATABASE_URL");
        services.AddDbContext<DbContexto>(options =>
        {
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });

        services.AddScoped<IBancoDeDadosServico<Cliente>, ClientesServico>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            MapRoutes(endpoints);
            MapRoutesClientes(endpoints);
        });
    }

    #region Rotas utilizando Minimal API

    public void MapRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => new {Mensagem = "Bem-vindo à API"})
            .Produces<dynamic>(StatusCodes.Status200OK)
            .WithName("Home")
            .WithTags("Testes");

        app.MapGet("/recebe-parametro", (string? nome) => 
        {
            if(string.IsNullOrEmpty(nome))
            {
                return Results.BadRequest(new {
                    Mensagem = "Olha, você não mandou uma informação importante. O nome é obrigatório!"
                });
            }

            nome = $""" 
            Alterando parametro recebido {nome}
            """;

            var objetoDeRetono = new {
                ParametroPassado = nome,
                Mensagem = "Muito bem alunos! Passamos um parâmetro por query string"
            };

            return Results.Created($"/recebe-parametro/{objetoDeRetono.ParametroPassado}", objetoDeRetono);
        })
        .Produces<dynamic>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("TesteRecebeParametro")
        .WithTags("Testes");
    }


    public void MapRoutesClientes(IEndpointRouteBuilder app)
    {
        app.MapGet("/clientes", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico) => 
        {
            var clientes = await clientesServico.Todos();
            return Results.Ok(clientes);
        })
        .Produces<List<Cliente>>(StatusCodes.Status200OK)
        .WithName("GetClientes")
        .WithTags("Clientes");

        app.MapPost("/clientes", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromBody] ClienteDTO clienteDTO) => 
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email,
            };
            // ClienteService.Salvar(cliente);
            await clientesServico.Salvar(cliente);

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        })
        .Produces<Cliente>(StatusCodes.Status201Created)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PostClientes")
        .WithTags("Clientes");

        app.MapPut("/clientes/{id}", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id, [FromBody] ClienteDTO clienteDTO) => 
        {
            var clienteDb = await clientesServico.BuscaPorId(id);
            if(clienteDb is null)
            {
                return Results.NotFound(new Error 
                { 
                    Codigo = 423, 
                    Mensagem = $"Cliente não encontrado com o id {id}" 
                });
            }

            var cliente = new Cliente
            {
                Id = id,
                Nome = clienteDTO.Nome,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email,
            };

            await clientesServico.Salvar(cliente);

            return Results.Ok(cliente);
        })
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PutClientes")
        .WithTags("Clientes");

        app.MapPatch("/clientes/{id}", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id, [FromBody] ClienteNomeDTO clienteNomeDTO) => 
        {
            var clienteDb = await clientesServico.BuscaPorId(id);
            if(clienteDb is null)
            {
                return Results.NotFound(new Error 
                { 
                    Codigo = 2345, 
                    Mensagem = $"Cliente não encontrado com o id {id}" 
                });
            }

            clienteDb.Nome = clienteNomeDTO.Nome;
            await clientesServico.Salvar(clienteDb);

            return Results.Ok(clienteDb);
        })
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PatchClientes")
        .WithTags("Clientes");

        app.MapDelete("/clientes/{id}", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id) => 
        {
            var clienteDb = await clientesServico.BuscaPorId(id);
            if(clienteDb is null)
            {
                return Results.NotFound(new Error 
                { 
                    Codigo = 22345, 
                    Mensagem = $"Cliente não encontrado com o id {id}" 
                });
            }

            await clientesServico.Excluir(clienteDb);

            return Results.NoContent();
        })
        .Produces<Cliente>(StatusCodes.Status204NoContent)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .WithName("DeleteClientes")
        .WithTags("Clientes");

        app.MapGet("/clientes/{id}", async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id) => 
        {
            var clienteDb = await clientesServico.BuscaPorId(id);
            if(clienteDb is null)
            {
                return Results.NotFound(new Error 
                { 
                    Codigo = 21345, 
                    Mensagem = $"Cliente não encontrado com o id {id}" 
                });
            }

            return Results.Ok(clienteDb);
        })
        .Produces<Cliente>(StatusCodes.Status204NoContent)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .WithName("GetClientesPorId")
        .WithTags("Clientes");
    }

    #endregion
}