using System.ComponentModel;
using System.Text.Json;

namespace Programa.Infra.Interfaces;

public class CsvDriver : IPersistence
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
    public void Delete(object objeto)
    {
        throw new NotImplementedException();
    }

    public List<object> FindAll()
    {
        throw new NotImplementedException();
    }

    public List<object> FindById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task Save(object objeto)
    {
        var csvLines = new List<string>();
        var props = TypeDescriptor.GetProperties(objeto).OfType<PropertyDescriptor>();
        var header = string.Join(";", props.ToList().Select(x => x.Name));

        csvLines.Add(header);

        var list = new List<object>();
        list.Add(objeto);

        var valueLines = list.Select(row => string.Join(";", header.Split(';').Select(a => row.GetType()?.GetProperty(a)?.GetValue(row, null))));
        csvLines.AddRange(valueLines);

        var csvString = string.Empty;
        foreach(var line in csvLines)
        {
            csvString += line + "\n";
        }

        var name = objeto.GetType().Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordLocal()}/{name}s.csv", csvString);
    }

    public void Update(string id, object objeto)
    {
        throw new NotImplementedException();
    }
}