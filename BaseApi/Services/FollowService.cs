using BaseApi.Models;
using BaseApi.Repositories;

namespace BaseApi.Services
{
    public class FollowService
    {
        private readonly FollowRepository _repository;

        public FollowService(FollowRepository repository)
        {
            _repository = repository;
        }

        public Follow CreateFollow(Follow newFollow)
        {
            return _repository.CreateFollow(newFollow);
        }
        public bool UpdateFollow(int id, Follow updatedFollow)
        {
            return _repository.UpdateFollow(id, updatedFollow);
        }
        public bool DeleteFollow(int id)
        {
            return _repository.DeleteFollow(id);
        }
        public Follow GetFollowById(int id)
        {
            return _repository.GetFollowById(id);
        }
        public IEnumerable<Follow> GetAllFollows()
        {
            return _repository.GetAllFollows();
        }
    }
}
