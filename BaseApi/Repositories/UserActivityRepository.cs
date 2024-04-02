using BaseApi.Models;
using MySqlConnector;
using Action = BaseApi.Models.Action;

namespace BaseApi.Repositories
{
    public class UserActivityRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserActivityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MyConn");
        }

        public async Task<IEnumerable<UserActivity>> ReportOfOneMonth(int userId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT userId, Id, Username, Timestamp, Action, Details FROM UserActivity where userId = @userId AND Timestamp >= DATE_SUB(NOW(), INTERVAL 30 DAY)";
                cmd.Parameters.AddWithValue("@userId", userId);

                var userActivities = new List<UserActivity>();
                UserActivity currentUserActivity = null;

                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        if (currentUserActivity == null)
                        {
                            currentUserActivity = new UserActivity
                            {
                                userId = reader.GetInt32(0),
                                Id = reader.GetInt32(1),
                                Events = new List<Event>()
                            };
                            userActivities.Add(currentUserActivity);
                        }

                        currentUserActivity.Events.Add(new Event
                        {
                            Username = reader.GetString(2),
                            Timestamp = reader.GetDateTime(3),
                            Action = (Action)Enum.Parse(typeof(Action), reader["Action"].ToString()),
                            Details = reader.GetString(5)
                        });
                    }
                }
                return userActivities;
            }
        }
        public async Task<IEnumerable<UserActivity>> GetAllActivity()
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT userId, Id, Username, Timestamp, Action, Details FROM UserActivity WHERE Timestamp >= DATE_SUB(NOW(), INTERVAL 30 DAY)";
                var userActivities = new List<UserActivity>();

                using (var reader = cmd.ExecuteReader())
                {
                    var userActivityDict = new Dictionary<int, UserActivity>();

                    while (await reader.ReadAsync())
                    {
                        var userId = reader.GetInt32(0);

                        if (!userActivityDict.ContainsKey(userId))
                        {
                            var newUserActivity = new UserActivity
                            {
                                userId = userId,
                                Events = new List<Event>()
                            };
                            userActivityDict[userId] = newUserActivity;
                            userActivities.Add(newUserActivity);
                        }

                        userActivityDict[userId].Events.Add(new Event
                        {
                            Username = reader.GetString(2),
                            Timestamp = reader.GetDateTime(3),
                            Action = (Action)Enum.Parse(typeof(Action), reader["Action"].ToString()),
                            Details = reader.GetString(5)
                        });
                    }
                }
                return userActivities;
            }
        }

    }

}

