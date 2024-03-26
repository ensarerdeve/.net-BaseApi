using BaseApi.Models;
using BaseApi.MongoDB;
using BaseApi.Repositories;
using MongoDB.Driver;

namespace BaseApi.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        protected MongoDBModel _database;

        public IMongoCollection<Post> _collection { get; set; }

        public PostRepository(MongoDBModel database) : base(database)
        {
            base._database = database;
            _collection = database.GetCollection<Post>();
        }
 

        public async Task<IEnumerable<Post>> GetByUserName(string username)
        {
            return await _collection.Find(user => user.userName == username).ToListAsync();
        }
    }
}
