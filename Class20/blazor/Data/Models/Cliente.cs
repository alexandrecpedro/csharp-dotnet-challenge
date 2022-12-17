using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models;

public record Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome não pode ser vazio")]
    [StringLength(50, ErrorMessage = "O nome está muito grande.")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "O Telefone não pode ser vazio")]
    public string Telefone { get;set; } = default!;

    [Required(ErrorMessage = "O Email não pode ser vazio")]
    public string Email { get;set; } = default!;

    public DateTime DataCriacao { get;set; }
}
