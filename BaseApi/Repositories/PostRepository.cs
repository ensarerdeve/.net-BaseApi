using BaseApi.Models;
using BaseApi.MongoDB;
using MongoDB.Driver;

namespace BaseApi.Repository
{
    public class PostRepository : IRepository<Post>
    {
        private readonly MongoDBModel _database;
        private readonly IMongoCollection<Post> _collection;
        public PostRepository(MongoDBModel database)
        {
            _database = database;
            _collection = _database.Posts;
        }
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<Post> GetById(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Post> Create(Post newPost)
        {
            await _collection.InsertOneAsync(newPost);
            return newPost;
        }
        public async Task<bool> Update(Guid id, Post updatedPost)
        {
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
