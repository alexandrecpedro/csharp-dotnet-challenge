using System.Text.Json;

namespace Programa.Infra.Interfaces;

public class JsonDriver : IPersistence
{
    // ATTRIBUTE
    private string recordLocal = "";

    // CONSTRUCTOR
    public JsonDriver(string recordLocal)
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
        string jsonString = JsonSerializer.Serialize(objeto);

        var name = objeto.GetType().Name.ToLower();

        await File.WriteAllTextAsync($"{this.GetRecordLocal()}/{name}s.json", jsonString);
    }

    public void Update(string id, object objeto)
    {
        throw new NotImplementedException();
    }
}