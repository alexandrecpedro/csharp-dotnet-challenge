using System.ComponentModel.DataAnnotations;

namespace MinimalApi.DTOs;

public record ClienteDTO
{
    [Required]
    public string? Nome {get; set;}
    [Required]
    public string? Telefone {get; set;}
    public string? Email {get; set;}
}