using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class ItemService : IRepository<Item>
    {
        private readonly IRepository<Item> _repository;

        public ItemService(IRepository<Item> repository)
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


        public async Task<bool> Update(Guid id, Item updatedItem)
        {
            return await _repository.Update(id, updatedItem);
        }
    }
}
