using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.DTOs;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;
using MinimalApi.ModelViews;

namespace MinimalApi.Routes;

internal struct ClientesRoute
{
    internal static void MapRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/clientes", [Authorize] [Authorize(Roles = "editor, administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico) => 
        {
            var clientes = await clientesServico.Todos();
            return Results.Ok(clientes);
        })
        .Produces<List<Cliente>>(StatusCodes.Status200OK)
        .WithName("GetClientes")
        .WithTags("Clientes");

        app.MapPost("/clientes", [Authorize] [Authorize(Roles = "editor, administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromBody] ClienteDTO clienteDTO) => 
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email,
            };
            await clientesServico.Salvar(cliente);

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        })
        .Produces<Cliente>(StatusCodes.Status201Created)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PostClientes")
        .WithTags("Clientes");

        app.MapPut("/clientes/{id}", [Authorize] [Authorize(Roles = "editor, administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id, [FromBody] ClienteDTO clienteDTO) => 
        {
            if (string.IsNullOrEmpty(clienteDTO.Nome))
            {
                return Results.BadRequest(new Error
                {
                    Codigo = 123432,
                    Mensagem = $"O Nome é obrigatório"
                });
            }

            var clienteDb = await clientesServico.BuscaPorId(id);
            if(clienteDb is null)
            {
                return Results.NotFound(new Error 
                { 
                    Codigo = 423, 
                    Mensagem = $"Cliente não encontrado com o id {id}" 
                });
            }

            await clientesServico.Update(clienteDb, clienteDTO);

            return Results.Ok(clienteDb);
        })
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .WithName("PutClientes")
        .WithTags("Clientes");

        app.MapPatch("/clientes/{id}", [Authorize] [Authorize(Roles = "editor, administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id, [FromBody] ClienteNomeDTO clienteNomeDTO) => 
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

        app.MapDelete("/clientes/{id}", [Authorize] [Authorize(Roles = "administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id) => 
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


        app.MapGet("/clientes/{id}", [Authorize] [Authorize(Roles = "editor, administrador")] async ([FromServices] IBancoDeDadosServico<Cliente> clientesServico, [FromRoute] int id) => 
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
}