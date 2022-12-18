using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.DTOs;

public record LoginDTO
{
    [Required(ErrorMessage = "O Email não pode ser vazio")]
    public string Email { get;set; } = default!;
    [Required(ErrorMessage = "A Senha não pode ser vazia")]
    public string Senha { get;set; } = default!;
}