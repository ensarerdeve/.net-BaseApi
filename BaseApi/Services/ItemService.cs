using BaseApi.Models;
using BaseApi.Repositories;

namespace BaseApi.Service
{
    public class ItemService
    {
        private readonly ItemRepository _repository;
        private readonly UserRepository _userRepository;

        public ItemService(ItemRepository repository, UserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Item> Create(Item newItem)
        {
            newItem.Id = Guid.NewGuid();
            var user = await _userRepository.GetByUsername(newItem.itemOwner);
            if (newItem.itemOwner == user.Username)
            {
            return await _repository.Create(newItem);
            }
            else
            {
                throw new KeyNotFoundException("Ürün oluşturulamaz");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var itemCheck = await _repository.GetById(id);
            if (itemCheck == null)
            {
                throw new KeyNotFoundException("Ürün bulunamadı.");
            }
            return await _repository.Delete(id);
        }


        public async Task<IEnumerable<Item>> GetAll()
        {
            var item = await _repository.GetAll();
            return item.OrderBy(u => u.itemName);
        }

        public async Task<Item> GetById(Guid id)
        {
            var item = await _repository.GetById(id);
            if (item != null)
            {
                return item;

            }
            else
            {
                throw new KeyNotFoundException("Böyle bir ürün yok.");
            }
        }

        public async Task<IEnumerable<Item>> GetByItemName(string itemName)
        {
            var item = await _repository.GetByItemName(itemName);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException($"{itemName} adlı ürün bulunamadı.");
            }
        }

        public async Task<IEnumerable<Item>> GetByPrice(string price)
        {
            var items = await _repository.GetByPrice(price);
            if (items != null)
            {
                return items;
            }
            else
            {
                throw new KeyNotFoundException("Belirtilen fiyatta ürünler bulunamadı");
            }
        }

        public async Task<bool> Update(Guid id, Item updatedItem)
        {
            return await _repository.Update(id, updatedItem);
        }
    }
}
