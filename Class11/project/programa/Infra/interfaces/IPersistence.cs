using System.Diagnostics.CodeAnalysis;

namespace Programa.Infra.Interfaces;

public interface IPersistence<T>
{
    // METHODS
    Task Save(T objeto);
    Task Delete(T objeto);
    Task DeleteAll();
    Task<List<T>> FindAll();
    Task<T?> FindById(string id);

    string GetRecordLocal();
}