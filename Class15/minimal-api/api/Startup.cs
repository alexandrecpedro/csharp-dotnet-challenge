using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MinimalApi.DTOs;
using MinimalApi.Models;
using MinimalApi.ModelViews;

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

        //services.AddScoped<IStudentsService, StudentsService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API Desafio v1"));
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
        app.MapGet("/", () => new {Mensagem = "Bem vindo a API"})
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
        app.MapGet("/clientes", () => 
        {
            var clientes = new List<Cliente>();
            // var clientes = ClienteService.Todos();

            return Results.Ok(clientes);
        })
        .Produces<List<Cliente>>(StatusCodes.Status200OK)
        .WithName("GetClientes")
        .WithTags("Clientes");

        app.MapPost("/clientes", ([FromBody] ClienteDTO clienteDTO) => 
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email,
            };
            // ClienteService.Salvar(cliente);

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        })
        .Produces<Cliente>(StatusCodes.Status201Created)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PostClientes")
        .WithTags("Clientes");

        app.MapPut("/clientes/{id}", ([FromRoute] int id, [FromBody] ClienteDTO clienteDTO) => 
        {
            if(string.IsNullOrEmpty(clienteDTO.Nome))
            {
                return Results.BadRequest(new Error 
                { 
                    Codigo = 123432, 
                    Mensagem = "O Nome é obrigatório" 
                });
            }

            /*
            var cliente = ClienteService.BuscaPorId(id);
            if(cliente == null)
                return Results.NotFound(new Error { Codigo = 123432, Mensagem = "Você passou um cliente inexistente" });
            cliente.Nome = clienteDTO.Nome;
            cliente.Telefone = clienteDTO.Telefone;
            cliente.Email = clienteDTO.Email;
            ClienteService.Update(cliente);
            */

            var cliente = new Cliente();

            return Results.Ok(cliente);
        })
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PutClientes")
        .WithTags("Clientes");

        // TODO Path
        // TODO Delete
        // TODO Get Cliente por ID

        // TODO fazer testes request
        // TODO fazer testes com postman
        // TODO fazer testes via curl

    }

    #endregion
}