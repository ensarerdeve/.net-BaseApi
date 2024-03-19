using BaseApi.Models;
using BaseApi.Repositories;
using System.Text.RegularExpressions;

namespace BaseApi.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return users.OrderBy(u => u.Name);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new KeyNotFoundException("Böyle bir kullanıcı yok.");
            }
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
            var userControl = await _userRepository.GetById(id);
            if (userControl != null)
            {
                return await _userRepository.Delete(id);

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir user zaten yok.");
            }
        }

        public async Task<IEnumerable<User>> GetByName(string name)
        {
            var userControl = await _userRepository.GetByName(name);
            if (userControl != null)
            {
                return userControl;

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir kullanıcı yok.");
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            var userControl = await _userRepository.GetByUsername(username);
            if (userControl != null)
            {
                return userControl;
            }
            else
            {
                throw new KeyNotFoundException("Kullanıcı bulunamadı.");
            }
        }
    }
}