using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetByUserName(string username);
    }
}
