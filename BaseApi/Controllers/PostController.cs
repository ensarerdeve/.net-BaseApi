using BaseApi.Models;
using BaseApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("Posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService postService;
        public PostController(PostService postService)
        {
            this.postService = postService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        {
            var posts = await postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPostsById(Guid id)
        {
            var post = await postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Posts newPost)
        {
            await postService.Create(newPost);
            return Ok(newPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, Posts updatedPost)
        {
            await postService.Update(id, updatedPost);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await postService.Delete(id);
            return Ok();
        }

    }
}
