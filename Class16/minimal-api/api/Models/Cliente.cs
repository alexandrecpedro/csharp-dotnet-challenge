namespace MinimalApi.Models;

public record Cliente
{
    // ATTRIBUTES
    public int Id {get; set; }
    public string? Nome {get; set; }
    public string? Telefone {get; set; }
    public string? Email {get; set; }
    public DateTime DataCriacao { get;set; }

    // CONSTRUCTOR
    public Cliente()
    {
        this.DataCriacao = DateTime.Now;
    }
}