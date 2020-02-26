using MongoDB.Driver;

namespace GS_Finance_Server.Interfaces.Repositories
{
    public interface IMongoRepository
    {
        MongoClient GetClient();
        IMongoDatabase GetDB();
    }
}