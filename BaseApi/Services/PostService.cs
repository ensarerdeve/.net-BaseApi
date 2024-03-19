using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class PostService : IPostRepository
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository) 
        {
            _postRepository = postRepository;
        }
        public async Task<Post> Create(Post newPost)
        {
            newPost.Id = Guid.NewGuid();
            return await _postRepository.Create(newPost);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _postRepository.Delete(id);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post> GetById(Guid id)
        {
            return await _postRepository.GetById(id);
        }

        public Task<Post> GetByName(string username)
        {
            return _postRepository.GetByName(username);
        }

        public async Task<bool> Update(Guid id, Post updatedPost)
        {
            return await _postRepository.Update(id, updatedPost);
        }
    }
}
