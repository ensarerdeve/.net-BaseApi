using BaseApi.Models;
using BaseApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("Item")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemsById(Guid id)
        {
            var item = await _itemService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpGet("/price/{price}")]
        public async Task<ActionResult<Item>> GetItemsByPrice(string price)
        {
            var item = await _itemService.GetByPrice(price);
            if (item == null)
            {
                return NotFound("Böyle bir ürün yok.");
            }
            return Ok(item);
        }
        [HttpGet("/itemName/{itemName}")]
        public async Task<ActionResult<Item>> GetItemsByItemName(string itemName)
        {
            var item = await _itemService.GetByItemName(itemName);
            if (item == null)
            {
                return NotFound("Böyle bir ürün yok.");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(Item newItem)
        {
            await _itemService.Create(newItem);
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Item updatedItem)
        {
            await _itemService.Update(id, updatedItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            await _itemService.Delete(id);
            return Ok();
        }
    }
}
