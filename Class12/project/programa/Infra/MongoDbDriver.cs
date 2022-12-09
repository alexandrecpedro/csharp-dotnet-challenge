using System.ComponentModel;
using System.Text.Json;
using MongoDB.Driver;
using Programa.Infra.Interfaces;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace Programa.Infra;

public class MongoDbDriver<T> : IPersistence<T>
{
    // ATTRIBUTES
    private string recordLocal = "";
    private IMongoDatabase mongoDatabase;

    // CONSTRUCTOR
    public MongoDbDriver(string recordLocal)
    {
        this.recordLocal = recordLocal;
        var cnn = this.recordLocal.Split('#');
        this.mongoDatabase = new MongoClient(cnn[0]).GetDatabase(cnn[1]);
    }

    // METHODS
    private IMongoCollection<T> mongoCollection()
    {
        return this.mongoDatabase.GetCollection<T>($"{typeof(T).Name.ToLower()}s");
    }

    public string GetRecordLocal()
    {
        return this.recordLocal;
    }

    public async Task Delete(T objeto)
    {
        await this.mongoCollection().DeleteOneAsync(p=> ((ICollectionMongoDb)p).ID == ((ICollectionMongoDb)objeto).ID);
    }

    public async Task DeleteAll()
    {
        foreach(var obj in await FindAll())
        {
            if(obj == null) continue;
            var objContrato = (ICollectionMongoDb)obj;
            await this.mongoCollection().DeleteOneAsync(p=> ((ICollectionMongoDb)p).ID == objContrato.ID);
        }
    }

    public async Task<List<T>> FindAll()
    {
        return await this.mongoCollection().AsQueryable().ToListAsync();
    }

    public async Task<T?> FindById(string id)
    {
        return await this.mongoCollection().AsQueryable().Where(p => ((ICollectionMongoDb)p).ID == ObjectId.Parse(id)).FirstAsync();
    }

    public async Task Save(T objeto)
    {
        ObjectId id = ((ICollectionMongoDb)objeto).ID;
        var objDb = await FindById(id.ToString());
        if(objDb == null) await this.mongoCollection().InsertOneAsync(objeto);
        else await this.mongoCollection().ReplaceOneAsync(c => ((ICollectionMongoDb)c).ID == ((ICollectionMongoDb)objeto).ID, objeto);
    }
}