using BaseApi.Models;
using BaseApi.MongoDB;
using BaseApi.Repository;
using MongoDB.Driver;

namespace BaseApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : IModel
    {
        protected MongoDBModel _database;
        private readonly IMongoCollection<T> _collection;
        public Repository(MongoDBModel database)
        {
            _database = database;
            _collection = _database.GetCollection<T>();
           
        }

        public async Task<T> Create(T newT)
        {
            await _collection.InsertOneAsync(newT);
            return newT;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Guid id, T updatedT)
        {
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, updatedT);
            return result.ModifiedCount > 0;
        }
    }
}
