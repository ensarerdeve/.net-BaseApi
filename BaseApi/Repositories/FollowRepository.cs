using BaseApi.Models;
using MySqlConnector;

namespace BaseApi.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public FollowRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MyConn");
        }
        public async Task<Follow> CreateFollow(Follow newFollow)
        {
            using(var conn = new MySqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Follow(id, followedUsername, user) VALUES(@id, @followedUsername, @user)";
                cmd.Parameters.AddWithValue("id", newFollow.id);
                cmd.Parameters.AddWithValue("followedUsername", newFollow.followedUsername);
                cmd.Parameters.AddWithValue("user", newFollow.user);
                cmd.ExecuteNonQuery();
                return newFollow;
            }
        }

        public async Task<bool> DeleteFollow(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM Follow WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                int rowsAffected=cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Follow";

                var follows = new List<Follow>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var follow = new Follow
                        {
                            id = reader.GetInt32(0),
                            followedUsername = reader.GetString(1),
                            user = reader.GetString(2)
                        };
                        follows.Add(follow);
                    }
                }
                return follows;                
            }
        }

        public async Task<Follow> GetFollowById(int id)
        {
            using(var conn = new MySqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT id, followedUsername, user FROM Follow WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Follow
                    {
                        id = reader.GetInt32(0),
                        followedUsername = reader.GetString(1),
                        user = reader.GetString(2)
                    };
                }
            }
        }

        public async Task<bool> UpdateFollow(int id, Follow updatedFollow)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Follow SET followedUsername = @followedUsername, user = @user WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@followedUsername", updatedFollow.followedUsername);
                cmd.Parameters.AddWithValue("@user", updatedFollow.user);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
