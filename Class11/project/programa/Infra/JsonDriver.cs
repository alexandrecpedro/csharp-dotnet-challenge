using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using Programa.Infra.Interfaces;

namespace Programa.Infra;

public class JsonDriver<T> : IPersistence<T>
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
    public async Task Delete(T objeto)
    {
        var list = await FindAll();
        list.Remove(objeto);
        await saveList(list);
    }

    public async Task DeleteAll()
    {
        var name = typeof(T).Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordLocal()}/{name}s.json", "[]");
    }

    public async Task<List<T>> FindAll()
    {
        var name = typeof(T).Name.ToLower();

        var file = $"{this.GetRecordLocal()}/{name}s.json";

        if (!File.Exists(file)) return new List<T>();

        string jsonString = await File.ReadAllTextAsync(file);
        var list = JsonSerializer.Deserialize<List<T>>(jsonString);
        return list ?? new List<T>();
    }

    public async Task<T?> FindById(string id)
    {
        var list = await FindAll();
        return findListId(list, id);
    }

    private T? findListId([NotNull] List<T> list, string id)
    {
        return list.Find(o => o?.GetType().GetProperty("ID")?.GetValue(o)?.ToString() == id);
    }

    // BEFORE
    // private T findListId([NotNull] List<T> list, string id)
    // {
    //     return list.Find(o => o.GetType().GetProperty("Id")?.GetValue(o)?.ToString() == id);
    // }

    public async Task Save(T objeto)
    {
        if (objeto == null) return;

        var list = await FindAll();
        var id = objeto.GetType().GetProperty("ID")?.GetValue(objeto)?.ToString();

        if (string.IsNullOrEmpty(id)) return;
        
        var objList = findListId(list, id);
        if (objList == null || objList.GetType().GetProperty("ID")?.GetValue(objList)?.ToString() == null) list.Add(objeto);
        else updateProperties(objeto, objList);

        await saveList(list);
    }

    private async Task saveList(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list);
        var name = typeof(T).Name.ToLower();
        await File.WriteAllTextAsync($"{this.GetRecordLocal()}/{name}s.json", jsonString);
    }

    private void updateProperties(T objetoFrom, T objListTo)
    {
        if (objetoFrom == null || objListTo == null) return;

        foreach (var propFrom in objetoFrom.GetType().GetProperties())
        {
            var propTo = objListTo.GetType().GetProperty(propFrom.Name);
            if (propTo != null)
            {
                var valueFrom = propFrom.GetValue(objetoFrom);
                propTo.SetValue(objListTo, valueFrom);
            }
        }
    }
}