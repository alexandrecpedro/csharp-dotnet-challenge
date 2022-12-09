using Programa.Infra.Interfaces;
using MongoDB.Bson;

namespace Programa.Test.Infra.Entidades;

public record ContaCorrenteMongoDb : ICollectionMongoDb
{
    public ObjectId ID { get; set; }
    public required ObjectId IdCliente { get; set; }
    public double Valor { get; set; } = default!;
    public DateTime Data { get; set; } = default!;
}