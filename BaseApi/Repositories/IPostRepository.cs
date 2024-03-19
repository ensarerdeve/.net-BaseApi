using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(Guid id);
        Task<Post> GetByName(string username);
        Task<Post> Create(Post newPost);
        Task<bool> Update(Guid id, Post updatedPost);
        Task<bool> Delete(Guid id);
    }
}
