using System;

namespace Programa.Models;

public record ContaCorrente
{
    public required string ID { get; set; }
    public required string IdCliente { get; set; }
    public double Valor { get; set; } = default!;
    public DateTime Data { get; set; } = default!;
}