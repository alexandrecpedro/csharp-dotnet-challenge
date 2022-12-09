using System.ComponentModel;
using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class AdoMySqlDriver<T> : IPersistence<T>
{
    // ATTRIBUTE
    private string recordLocal = "";

    // CONSTRUCTOR
    public AdoMySqlDriver(string recordLocal)
    {
        this.recordLocal = recordLocal;
    }
    
    // METHODS
    public string GetRecordLocal()
    {
        return this.recordLocal;
    }
    public async Task Delete(T objeto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAll()
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> FindAll()
    {
        throw new NotImplementedException();
    }

    public async Task<T?> FindById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task Save(T objeto)
    {
        throw new NotImplementedException();
    }
}