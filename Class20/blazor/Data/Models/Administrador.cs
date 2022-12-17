namespace Blazor.Data.Models;

public record Administrador
{
    public string Email { get;set; } = default!;
    public string Permissao { get;set; } = default!;
    public string Token { get;set; } = default!;
}