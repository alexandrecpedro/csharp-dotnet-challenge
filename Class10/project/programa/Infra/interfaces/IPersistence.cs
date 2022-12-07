namespace Programa.Infra.Interfaces;

public interface IPersistence
{
    // METHODS
    Task Save(object objeto);
    void Update(string id, object objeto);
    void Delete(object objeto);
    List<object> FindAll();
    List<object> FindById(string id);

    string GetRecordLocal();
}