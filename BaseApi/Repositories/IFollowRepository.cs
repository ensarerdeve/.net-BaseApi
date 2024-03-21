using BaseApi.Models;

namespace BaseApi.Repositories
{
    public interface IFollowRepository
    {
        IEnumerable<Follow> GetAllFollows();
        Follow GetFollowById(int id);
        Follow CreateFollow(Follow newFollow);
        bool UpdateFollow(int id, Follow updatedFollow);
        bool DeleteFollow(int id);

    }
}
