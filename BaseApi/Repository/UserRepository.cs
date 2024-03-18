using BaseApi.Models;
using BaseApi.MongoDB;
using MongoDB.Driver;

namespace BaseApi.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly MongoDBModel _database;
        private readonly IMongoCollection<User> _collection;

        public UserRepository(MongoDBModel database)
        {
            _database = database;
            _collection = _database.Users;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User newUser)
        {
            newUser.Id = Guid.NewGuid();
            await _collection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<bool> Update(Guid id, User updatedUser)
        {
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, updatedUser);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
