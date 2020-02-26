using GS_Finance_Server.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace GS_Finance_Server.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private MongoClient _defaultClient = null;
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public MongoRepository(IConfiguration configuration, ILogger logger)
        {
            _connectionString = configuration.GetConnectionString("mongodb");
            _logger = logger;
        }

        public MongoClient GetClient()
        {
            if (_defaultClient == null)
            {
                _logger.LogInformation($"MongoDB getting client: {_connectionString}");
                _defaultClient = new MongoClient(_connectionString);
            }

            return _defaultClient;
        }

        public IMongoDatabase GetDB()
        {
            return GetClient().GetDatabase("finance");
        }
    }
}