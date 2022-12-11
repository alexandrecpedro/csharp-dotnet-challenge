namespace MinimalApi.DTOs;

public record ClienteDTO
{
    public string? Nome {get; set;}
    public string? Telefone {get; set;}
    public string? Email {get; set;}
}