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

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Item> Items => _database.GetCollection<Item>("Items");

    }
}
