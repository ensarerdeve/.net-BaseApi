using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByName(string username);
        Task<User> Create(User newUser);
        Task<bool> Update(Guid id, User updatedUser);
        Task<bool> Delete(Guid id);

    }
}
