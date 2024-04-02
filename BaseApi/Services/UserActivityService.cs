using BaseApi.Models;
using BaseApi.Repositories;
using Action = BaseApi.Models.Action;

namespace BaseApi.Services
{
    public class UserActivityService
    {
        public class UserActivityReport
        {
            public int userId { get; set; }
            public int userIdCount { get; set; }
            public int LoginCount { get; set; }
            public int LogoutCount { get; set; }
            public int UsernameChangedCount { get; set; }
        }

        private readonly UserActivityRepository _userActivityRepository;

        public UserActivityService(UserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<UserActivityReport> GetUserActivityReport(int userId)
        {
            try
            {
                var userActivityReport = new UserActivityReport();
                var userActivities = await _userActivityRepository.ReportOfOneMonth(userId);
                if(!userActivities.Any()) 
                {
                    throw new KeyNotFoundException("Belirtilen userId'de bir kullanıcı bulunamadı.");
                }
                userActivityReport.userId = userId;
                userActivityReport.LoginCount = userActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.Login);
                userActivityReport.LogoutCount = userActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.Logout);
                userActivityReport.UsernameChangedCount = userActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.UsernameChanged);

                return userActivityReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserActivityReport> GetReportsOfAllUsers()
        {
            var userActivityReport = new UserActivityReport();
            var usersActivities = await _userActivityRepository.GetAllActivity();
            userActivityReport.userIdCount = usersActivities.Distinct().Count(ua => ua.userId == ua.userId);
            userActivityReport.LoginCount = usersActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.Login);
            userActivityReport.LogoutCount = usersActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.Logout);
            userActivityReport.UsernameChangedCount = usersActivities.SelectMany(ua => ua.Events).Count(e => e.Action == Action.UsernameChanged);

            return userActivityReport;
        }
    }
}
