using BaseApi.Models;
using BaseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("UserActivity")]
    public class UserActivityController : ControllerBase
    {
        private readonly UserActivityService _userActivityService;

        public UserActivityController(UserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserActivity>> GetReport(int userId)
        {
            var report = await _userActivityService.GetUserActivityReport(userId);
            var reportText = $"Bu ay, Id'si {report.userId} olan kullanıcı {report.LoginCount} kez login işlemi, {report.LogoutCount} kez logout işlemi ve {report.UsernameChangedCount} kez kullanıcı adı değişikliği işlemi yaptı.";

            return Ok(reportText);
        }

        [HttpGet]
        public async Task<ActionResult<UserActivity>> GetAllReportsOfAllUsers()
        {
            var reports = await _userActivityService.GetReportsOfAllUsers();
            var reportText = $"Bu ay {reports.userId} tane kullanıcı {reports.LoginCount} kez login işlemi, {reports.LogoutCount} kez logout işlemi ve {reports.UsernameChangedCount} kez kullanıcı adı değişikliği işlemi yaptı.";

            return Ok(reportText);
        }

    }
}
