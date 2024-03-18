using BaseApi.Models;
using BaseApi.MongoDB;
using MongoDB.Driver;

namespace BaseApi.Repository
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly MongoDBModel _database;
        private readonly IMongoCollection<Item> _collection;
        public ItemRepository(MongoDBModel database)
        {
            _database = database;
            _collection = _database.Items;
        }


        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }


        public async Task<Item> GetById(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }


        public async Task<Item> Create(Item newItem)
        {
            await _collection.InsertOneAsync(newItem);
            return newItem;
        }


        public async Task<bool> Update(Guid id, Item updatedItems)
        {
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, updatedItems);
            return result.ModifiedCount > 0;
        }


        public async Task<bool> Delete(Guid id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
