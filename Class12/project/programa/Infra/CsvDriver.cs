using System.ComponentModel;
using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class CsvDriver<T> : IPersistence<T>
{
    // ATTRIBUTE
    private string recordLocal = "";

    // CONSTRUCTOR
    public CsvDriver(string recordLocal)
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

    async Task IPersistence<T>.DeleteAll()
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
        var csvLines = new List<string>();
        var props = typeof(T).GetProperties();
        var header = string.Join(";", props.ToList().Select(x => x.Name));

        csvLines.Add(header);

        var list = new List<T>();
        // var list = await FindAll();
        list.Add(objeto);

        var valueLines = list.Select(row => string.Join(";", header.Split(';').Select(a => row?.GetType()?.GetProperty(a)?.GetValue(row, null))));
        csvLines.AddRange(valueLines);

        var csvString = string.Empty;
        foreach(var line in csvLines)
        {
            csvString += line + "\n";
        }

        var name = objeto?.GetType().Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordLocal()}/{name}s.csv", csvString);
    }
}