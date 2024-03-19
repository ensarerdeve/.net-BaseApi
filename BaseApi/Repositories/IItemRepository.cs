using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetByUserName(string username);
        Task<IEnumerable<Item>> GetByItemName(string itemName);
        Task<IEnumerable<Item>> GetByPrice(string price);
    }
}
