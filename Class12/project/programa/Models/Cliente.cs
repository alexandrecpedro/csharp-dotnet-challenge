namespace Programa.Models;

// default = internal class (only known by programa.csproject)
public record Cliente
{
    public required string ID { get;set; }
    public string Nome { get;set; } = default!;
    public string Telefone { get;set; } = default!;
    public string Email { get;set; } = default!;
}