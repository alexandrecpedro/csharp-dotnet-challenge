using Microsoft.AspNetCore.Mvc;
using MinimalApi.DTOs;
using MinimalApi.Models;
using MinimalApi.ModelViews;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Mysql
#endregion

#region JWT
#endregion

#region Swagger
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MapRoutes(app);
MapRoutesClientes(app);

app.Run();

#region Rotas usando Minimal API

void MapRoutes(WebApplication app)
{
    app.MapGet("/", () => new {Mensagem = "Bem-vindo à API"})
        .Produces<dynamic>(StatusCodes.Status200OK)
        .WithName("Home")
        .WithTags("Testes");

    app.MapGet("/recebe-parametro", (string? nome) => 
    {
        if (string.IsNullOrEmpty(nome))
        {
            return Results.BadRequest(new {
                Mensagem = "Olha, você não mandou uma informação importante. O nome é obrigatório!"
            });
        }

        nome = $"""
        Alterando parâmetro recebido {nome}
        """;

        var objetoDeRetorno = new {
            ParametroPassado = nome,
            Mensagem = "Muito bem alunos! Passamos um parâmetro por query string"
        };

        return Results.Created($"/recebe-parametro/{objetoDeRetorno.ParametroPassado}", objetoDeRetorno);
    })
    .Produces<dynamic>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("TesteRecebeParametro")
    .WithTags("Testes");
}

void MapRoutesClientes(WebApplication app)
{
    app.MapGet("/clientes", () => 
    {
        var clientes = new List<Cliente>();
        // var clientes = ClienteService.Todos();
        return Results.Ok(clientes);
    })
    .Produces<Cliente>(StatusCodes.Status200OK)
    .Produces<Error>(StatusCodes.Status400BadRequest)
    .WithName("GetClientes")
    .WithTags("Clientes");

    // TODO Get Cliente por Id
    // app.MapGet("/clientes/{id}", ([FromRoute] int id) => 
    // {
    //     var clientes = new List<Cliente>();
    //     // var clientes = ClienteService.Todos();
    //     return Results.Ok(clientes);
    // })
    // .Produces<Cliente>(StatusCodes.Status200OK)
    // .Produces<Error>(StatusCodes.Status400BadRequest)
    // .WithName("GetClientePorId")
    // .WithTags("Clientes");

    app.MapPost("/clientes", ([FromBody] ClienteDTO clienteDTO) => 
    {
        var cliente = new Cliente 
        {
            Nome = clienteDTO.Nome,
            Telefone = clienteDTO.Telefone,
            Email = clienteDTO.Email
        };
        // ClienteService.Salvar(cliente);
        return Results.Created($"/clientes/{cliente.Id}", cliente);
    })
    .Produces<Cliente>(StatusCodes.Status201Created)
    .Produces<Error>(StatusCodes.Status400BadRequest)
    .WithName("PostClientes")
    .WithTags("Clientes");

    app.MapPut("/clientes/{id}", ([FromRoute] int id, [FromBody] ClienteDTO clienteDTO) => 
    {
        if (string.IsNullOrEmpty(clienteDTO.Nome))
        {
            return Results.BadRequest(new Error 
            { 
                Codigo = 12345,
                Mensagem = "O nome é obrigatório"
            });
        }
        /* 
        var cliente = ClienteService.BuscaPorId(id);
        if(cliente == null)
            return Results.NotFound(new Error { Codigo = 12345, Mensagem = "Você passou um cliente inexistente" });

        cliente.Nome = clienteDTO.Nome;
        cliente.Telefone = clienteDTO.Telefone;
        cliente.Email = clienteDTO.Email;
        ClienteService.Update(cliente);
        */
        
        var cliente = new Cliente();
        
        return Results.Ok(cliente);
    })
    .Produces<Cliente>(StatusCodes.Status200OK)
    .Produces<Error>(StatusCodes.Status400BadRequest)
    .Produces<Error>(StatusCodes.Status404NotFound)
    .WithName("PutClientes")
    .WithTags("Clientes");

    // TODO Path
    // TODO Delete
}

#endregion
