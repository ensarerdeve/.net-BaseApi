using BaseApi.Repositories;
using Action = BaseApi.Models.Action;

namespace BaseApi.Services
{
    public class UserActivityService
    {
        public class UserActivityReport
        {
            public int userId { get; set; }
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
                foreach (var userActivity in userActivities)
                {
                    foreach (var evt in userActivity.Events)
                    {
                        switch (evt.Action)
                        {
                            case Action.Login:
                                userActivityReport.LoginCount++;
                                break;
                            case Action.Logout:
                                userActivityReport.LogoutCount++;
                                break;
                            case Action.UsernameChanged:
                                userActivityReport.UsernameChangedCount++;
                                break;
                        }
                    }
                    userActivityReport.userId = userActivity.userId;
                }

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
            var uniqueUserIds = new HashSet<int>();
            foreach (var userActivity in usersActivities)
            {
                uniqueUserIds.Add(userActivity.userId);
                foreach (var evt in userActivity.Events)
                {
                    switch (evt.Action)
                    {
                        case Action.Login:
                            userActivityReport.LoginCount++;
                            break;
                        case Action.Logout:
                            userActivityReport.LogoutCount++;
                            break;
                        case Action.UsernameChanged:
                            userActivityReport.UsernameChangedCount++;
                            break;
                    }
                }
            }
            userActivityReport.userId = uniqueUserIds.Count;
            return userActivityReport;
        }
    }
}
