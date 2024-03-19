using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> GetById(Guid id);
        Task<Item> GetByPrice(string price);
        Task<Item> GetByItemName(string itemName);
        Task<Item> Create(Item newItem);
        Task<bool> Update(Guid id, Item updatedItem);
        Task<bool> Delete(Guid id);
    }
}
