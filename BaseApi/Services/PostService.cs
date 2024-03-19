using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        public PostService(PostRepository postRepository, UserRepository userRepository) 
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public async Task<Post> Create(Post newPost)
        {
            newPost.Id = Guid.NewGuid();
            var user = await _userRepository.GetByUsername(newPost.userName);
            if (newPost.userName == user.Username)
            {
                return await _postRepository.Create(newPost);
            }
            else if (user == null)
            {
                throw new KeyNotFoundException("Kullanıcı bulunamadı.");
            }
            else
            {
                throw new KeyNotFoundException("Post oluşturulamaz.");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var userControl = await _postRepository.GetById(id);
            if (userControl != null)
            {
                return await _postRepository.Delete(id);

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir post zaten yok.");
            }
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts =  await _postRepository.GetAll();
            return posts.OrderBy(p => p.createdAt);
        }

        public async Task<Post> GetById(Guid id)
        {
            var post = await _postRepository.GetById(id);
            if (post != null)
            {
                return post;

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir post yok.");
            }
        }

        public async Task<IEnumerable<Post>> GetByUserName(string username)
        {
            var postControl = await _postRepository.GetByUserName(username);
            if (postControl != null)
            {
                return postControl;

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir kullanıcının postu yok.");
            }
        }

        public async Task<bool> Update(Guid id, Post updatedPost)
        {
            return await _postRepository.Update(id, updatedPost);
        }
    }
}
