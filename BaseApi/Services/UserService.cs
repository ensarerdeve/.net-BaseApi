using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class UserService : IRepository<User>
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task <User> Create(User newUser)
        {
            newUser.Id = Guid.NewGuid();
            return await _userRepository.Create(newUser);
        }
        public async Task <bool> Update(Guid id, User updatedUser)
        {
            return await _userRepository.Update(id, updatedUser);
        }
        public async Task<bool> Delete(Guid id)
        {
           return await _userRepository.Delete(id);
        }
    }
}
