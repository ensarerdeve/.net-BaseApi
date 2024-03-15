using BaseApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseApi.MongoDB
{
    public class MongoDBModel
    {
        private readonly IMongoDatabase _database;

        public MongoDBModel(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Users> Users => _database.GetCollection<Users>("Users");
        public IMongoCollection<Posts> Posts => _database.GetCollection<Posts>("Posts");

    }
}
