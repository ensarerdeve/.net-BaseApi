using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class ItemService : IItemRepository
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item> Create(Item newItem)
        {
            newItem.Id = Guid.NewGuid();
            return await _repository.Create(newItem);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }


        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Item> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Item> GetByItemName(string itemName)
        {
            return await _repository.GetByItemName(itemName);
        }

        public async Task<Item> GetByPrice(string price)
        {
            return await _repository.GetByPrice(price);
        }

        public async Task<bool> Update(Guid id, Item updatedItem)
        {
            return await _repository.Update(id, updatedItem);
        }
    }
}
