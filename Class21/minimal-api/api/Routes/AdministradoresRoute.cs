using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.DTOs;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Models;
using MinimalApi.Services;

namespace MinimalApi.Routes;

internal struct AdministradoresRoute
{
    internal static void MapRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", [AllowAnonymous] async ( 
            [FromServices] ILogin<Administrador> administradoresServico,
            [FromBody] LoginDTO login
        ) => 
        {
            if(string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
            {
                return Results.BadRequest(new {
                    Mesagem = "Email e Senha é obrigatório"
                });
            }

            var admEncontrado = await administradoresServico.LoginAsync(login.Email, login.Senha);

            if(admEncontrado is null) return Results.Unauthorized();

            var adm = new AdministradorLogadoDTO
            {
                Email = admEncontrado.Email,
                Permissao = admEncontrado.Permissao,
                Token = TokenServico.Gerar(admEncontrado)
            };

            return Results.Ok(adm);
        })
        .Produces<dynamic>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("Login")
        .WithTags("Administradores");

        app.MapPost("/gerar-administrador-e-editor", [AllowAnonymous] async ( 
                [FromServices] ILogin<Administrador> administradoresServico
        ) => 
        {
            var admEncontrado = await administradoresServico.LoginAsync("danilo@torneseumprogramador.com.br", "123456");
            var editorEncontrado = await administradoresServico.LoginAsync("suporte@torneseumprogramador.com.br", "123456");

            if(admEncontrado is null)
            {
                await administradoresServico.Salvar(new Administrador
                {
                    Nome = "Danilo",
                    Email = "danilo@torneseumprogramador.com.br",
                    Senha = "123456",
                    Permissao = "administrador"
                });
            }

            if(editorEncontrado is null)
            {
                await administradoresServico.Salvar(new Administrador
                {
                    Nome = "Suporte",
                    Email = "suporte@torneseumprogramador.com.br",
                    Senha = "123456",
                    Permissao = "editor"
                });
            }

            return Results.Ok(new
            {
                Administrador = admEncontrado,
                Editor = editorEncontrado
            });
        })
        .Produces<dynamic>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("GeraADM")
        .WithTags("Administradores");


        app.MapGet("/administradores", [AllowAnonymous] async ( 
                [FromServices] ILogin<Administrador> administradoresServico
        ) => 
        {
            return Results.Ok(await administradoresServico.Todos());
        })
        .Produces<dynamic>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("ListaAdministradores")
        .WithTags("Administradores");
    }
}