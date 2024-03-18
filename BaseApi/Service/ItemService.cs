using BaseApi.Models;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public class ItemService : IRepository<Items>
    {
        private readonly IRepository<Items> _repository;

        public ItemService(IRepository<Items> repository)
        {
            _repository = repository;
        }

        public async Task<Items> Create(Items newItem)
        {
            newItem.Id = Guid.NewGuid();
            return await _repository.Create(newItem);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }


        public async Task<IEnumerable<Items>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Items> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }


        public async Task<bool> Update(Guid id, Items updatedItem)
        {
            return await _repository.Update(id, updatedItem);
        }
    }
}
