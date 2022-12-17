using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Models;

[Table("clientes")]
public record Cliente
{
    // ATTRIBUTES
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100)]
    public string? Nome {get; set; }

    [MaxLength(20)]
    [Required(ErrorMessage = "Telefone é obrigatório")]
    public string? Telefone {get; set; }

    [MaxLength(200)]
    public string? Email {get; set; }
    
    public DateTime DataCriacao { get;set; }

    // CONSTRUCTOR
    public Cliente()
    {
        this.DataCriacao = DateTime.Now;
    }
}