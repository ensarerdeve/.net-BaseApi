using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Service;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BaseApi.Services
{
    public class FollowService
    {
        private readonly FollowRepository _repository;
        private readonly UserService _userService;

        public FollowService(FollowRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Follow> CreateFollow(Follow newFollow)
        {
            var userControl = await _userService.GetByUsername(newFollow.user);
            var followerUserControl = await _userService.GetByUsername(newFollow.followedUsername);
            if (userControl == null || followerUserControl == null) 
            {
                throw new KeyNotFoundException("Böyle bir user yok.");
            }
            else
            {
                await _repository.CreateFollow(newFollow);
                return newFollow;
            }
            
        }
        public async Task<bool> UpdateFollow(int id, Follow updatedFollow)
        {
            var user = await _userService.GetByUsername(updatedFollow.user);
            var followedUsername = await _userService.GetByUsername(updatedFollow.followedUsername);
            if(user != null && followedUsername != null)
            {
                await _repository.UpdateFollow(id, updatedFollow);
                return true;
            }
            else
            {
                throw new KeyNotFoundException("Yanlış veya hatalı kullanıcı adı.");
            }
        }
        public async Task<bool> DeleteFollow(int id)
        {
            var checkId = _repository.GetFollowById(id);
            if (checkId != null)
            {
                await _repository.DeleteFollow(id);
                return true;
            }

            else
            {
                return false;
            }
        }
        public async Task<Follow> GetFollowById(int id)
        {
            var followCheck = await _repository.GetFollowById(id);
            if(followCheck != null)
            {
                return followCheck;
            }
            else
            {
                throw new KeyNotFoundException("Böyle bir takip bulunamadı.");   
            }
        }
        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            return await _repository.GetAllFollows();
        }
    }
}
