using BaseApi.Models;

namespace BaseApi.Repositories
{
    public interface IFollowRepository
    {
        Task<IEnumerable<Follow>> GetAllFollows();
        Task<Follow> GetFollowById(int id);
        Task<Follow> CreateFollow(Follow newFollow);
        Task<bool> UpdateFollow(int id, Follow updatedFollow);
        Task<bool> DeleteFollow(int id);

    }
}
