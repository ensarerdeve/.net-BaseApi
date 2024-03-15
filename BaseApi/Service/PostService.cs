using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class PostService : IRepository<Posts>
    {
        private readonly IRepository<Posts> _postRepository;
        public PostService(IRepository<Posts> postRepository) 
        {
            _postRepository = postRepository;
        }
        public async Task<Posts> Create(Posts newPost)
        {
            return await _postRepository.Create(newPost);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _postRepository.Delete(id);
        }

        public async Task<IEnumerable<Posts>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Posts> GetById(Guid id)
        {
            return await _postRepository.GetById(id);
        }

        public async Task<bool> Update(Guid id, Posts updatedPost)
        {
            return await _postRepository.Update(id, updatedPost);
        }
    }
}
