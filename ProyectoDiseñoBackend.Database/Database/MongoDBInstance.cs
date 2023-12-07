using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ProyectoDiseñoBackend.Database
{
    public sealed class MongoDBInstance
    {
        private static IMongoDatabase _database;
        private static MongoClient _client;

        public MongoDBInstance(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDB");
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(configuration["ProyectDatabase:DatabaseName"]);
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
