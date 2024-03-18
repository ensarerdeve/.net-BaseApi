using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class UserService : IRepository<Users>
    {
        private readonly IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<Users> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task <Users> Create(Users newUser)
        {
            newUser.Id = Guid.NewGuid();
            return await _userRepository.Create(newUser);
        }
        public async Task <bool> Update(Guid id, Users updatedUser)
        {
            return await _userRepository.Update(id, updatedUser);
        }
        public async Task<bool> Delete(Guid id)
        {
           return await _userRepository.Delete(id);
        }
    }
}
