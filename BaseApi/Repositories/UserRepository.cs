using BaseApi.Models;
using BaseApi.MongoDB;
using MongoDB.Driver;

namespace BaseApi.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected MongoDBModel _database;

        public IMongoCollection<User> _collection { get; set; }

        public UserRepository(MongoDBModel database) : base(database)
        {
            base._database = database;
            _collection = database.GetCollection<User>();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _collection.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetByName(string name)
        {
            return await _collection.Find(user => user.Name == name).ToListAsync();
        }
    }
}

