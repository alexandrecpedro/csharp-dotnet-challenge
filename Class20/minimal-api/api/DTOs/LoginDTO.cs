using System.ComponentModel.DataAnnotations;

namespace MinimalApi.DTOs;

public record LoginDTO
{
    [Required]
    public string Email { get;set; } = default!;
    public string Senha { get;set; } = default!;
}