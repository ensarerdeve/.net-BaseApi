using BaseApi.Models;
using System.Data.SqlClient;

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
        public Follow CreateFollow(Follow newFollow)
        {
            using(var conn = new SqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Follow(id, followedUsername) VALUES(@id, @followedUsername)";
                cmd.Parameters.AddWithValue("id", newFollow.id);
                cmd.Parameters.AddWithValue("followedUsername", newFollow.followedUsername);
                cmd.ExecuteNonQuery();
                return newFollow;
            }
        }

        public bool DeleteFollow(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM Follow WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                int rowsAffected=cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public IEnumerable<Follow> GetAllFollows()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Follow";

                var follows = new List<Follow>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        follows.Add(new Follow());
                    }
                }
                return follows;
            }
        }

        public Follow GetFollowById(int id)
        {
            using(var conn = new SqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT id, followedUsername FROM Follow WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Follow
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id")),
                        followedUsername = reader.GetString(reader.GetOrdinal("username"))
                    };
                }
            }
        }

        public bool UpdateFollow(int id, Follow updatedFollow)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Follow SET followedUsername = @followedUsername WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@newFollowerName", updatedFollow.followedUsername);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
