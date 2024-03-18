using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class PostService : IRepository<Post>
    {
        private readonly IRepository<Post> _postRepository;
        public PostService(IRepository<Post> postRepository) 
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

        public async Task<bool> Update(Guid id, Post updatedPost)
        {
            return await _postRepository.Update(id, updatedPost);
        }
    }
}
