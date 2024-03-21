using BaseApi.Models;
using BaseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("Follow")]
    public class FollowController : ControllerBase
    {
        private readonly FollowService _followService;

        public FollowController(FollowService followService)
        {
            _followService = followService;
        }
        [HttpGet]
        public IEnumerable<Follow> Get()
        {
            return _followService.GetAllFollows();
        }
    }
}
