﻿using BaseApi.Models;
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
            var follow = await _followService.GetFollowById(id);
            return Ok(follow);
        }
        [HttpPost]
        public async Task<ActionResult> CreateFollow(Follow newFollow)
        {
            var createdFollow = await _followService.CreateFollow(newFollow);
            return Ok(createdFollow);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFollow(int id ,Follow updatedFollow)
        {
            var follow = await _followService.UpdateFollow(id, updatedFollow);
            return Ok(follow);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteFollow = await _followService.DeleteFollow(id);
            return Ok(deleteFollow);
        }
    }
}
