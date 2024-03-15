using BaseApi.Models;
using BaseApi.MongoDB;
using MongoDB.Driver;

namespace BaseApi.Repository
{
    public class PostRepository : IRepository<Posts>
    {
        private readonly MongoDBModel _database;
        private readonly IMongoCollection<Posts> _collection;
        public PostRepository(MongoDBModel database)
        {
            _database = database;
            _collection = _database.Posts;
        }
        public async Task<IEnumerable<Posts>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<Posts> GetById(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Posts> Create(Posts newPost)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            newPost.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            await _collection.InsertOneAsync(newPost);
            return newPost;
        }
        public async Task<bool> Update(Guid id, Posts updatedPost)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            updatedPost.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, updatedPost);
            return result.ModifiedCount > 0;
        }
        public async Task<bool> Delete(Guid id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
