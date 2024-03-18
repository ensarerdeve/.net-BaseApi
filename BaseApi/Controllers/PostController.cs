using BaseApi.Models;
using BaseApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("Post")]
    public class PostController : ControllerBase
    {
        private readonly PostService postService;
        public PostController(PostService postService)
        {
            this.postService = postService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostsById(Guid id)
        {
            var post = await postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post newPost)
        {
            await postService.Create(newPost);
            return Ok(newPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, Post updatedPost)
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
