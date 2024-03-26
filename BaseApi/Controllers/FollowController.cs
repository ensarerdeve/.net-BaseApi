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
        public async Task<ActionResult<IEnumerable<Follow>>> GetAllFollows()
        {
            var follows = await _followService.GetAllFollows();
            return Ok(follows);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollowById(int id) 
        {
            var follow = _followService.GetFollowById(id);
            return Ok(follow);
        }
        [HttpPost]
        public async Task<ActionResult> CreateFollow(Follow newFollow)
        {
            await _followService.CreateFollow(newFollow);
            return Ok(newFollow);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFollow(int id ,Follow updatedFollow)
        {
            await _followService.UpdateFollow(id, updatedFollow);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _followService.DeleteFollow(id);
            return Ok();
        }
    }
}
