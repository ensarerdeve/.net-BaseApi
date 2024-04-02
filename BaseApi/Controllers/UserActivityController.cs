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
        private readonly UserActivityServiceDTO _userActivityDTOService;

        public UserActivityController(UserActivityService userActivityService, UserActivityServiceDTO userActivityDTOService)
        {
            _userActivityService = userActivityService;
            _userActivityDTOService = userActivityDTOService;
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
            var reportText = $"Bu ay {reports.userIdCount} tane kullanıcı {reports.LoginCount} kez login işlemi, {reports.LogoutCount} kez logout işlemi ve {reports.UsernameChangedCount} kez kullanıcı adı değişikliği işlemi yaptı.";

            return Ok(reportText);
        }
        [HttpGet("DataCheck")]
        public async Task<IActionResult> ConvertReportsToJson()
        {
            var report = await _userActivityDTOService.Reports();
            if (report == true)
            {
                throw new KeyNotFoundException("Files are already exist.");
            }
            await _userActivityDTOService.GenerateYearlyReports();
            throw new KeyNotFoundException("Files are created.");
        }

        [HttpGet("monthlyReport/{monthNumber}")]
        public async Task<IActionResult> MonthlyCheck(int monthNumber)
        {
            var file = await _userActivityDTOService.ValidateMonthlyReport(monthNumber);
            if (file == true)
            {
                var filePath = $"D:\\datas\\{monthNumber}_report.json";
                var reportContent = await System.IO.File.ReadAllTextAsync(filePath);
                return Ok(reportContent);
            }
            else
            {
                var filePath = $"D:\\datas\\{monthNumber}_report.json";
                var reportContent = await System.IO.File.ReadAllTextAsync(filePath);
                return Ok(reportContent);
            }
        }

    }
}
