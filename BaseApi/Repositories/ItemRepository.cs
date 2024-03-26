using BaseApi.Models;
using BaseApi.MongoDB;
using BaseApi.Repository;
using MongoDB.Driver;
using System.Collections;

namespace BaseApi.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        protected MongoDBModel _database;

        public IMongoCollection<Item> _collection { get; set; }

        public ItemRepository(MongoDBModel database) : base(database)
        {
            base._database = database;
            _collection = database.GetCollection<Item>();
        }

        public async Task<IEnumerable<Item>> GetByUserName(string username)
        {
            return await _collection.Find(user => user.itemOwner == username).ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetByItemName(string itemName)
        {
            return await _collection.Find(item => item.itemName == itemName).ToListAsync();
        }
        public async Task<IEnumerable<Item>> GetByPrice(string price)
        {
            return await _collection.Find(item => item.itemPrice == price).ToListAsync();
        }

    }
}
