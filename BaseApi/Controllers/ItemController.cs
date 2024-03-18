using BaseApi.Models;
using BaseApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items>>> GetAllItems()
        {
            var items = await _itemService.GetAll();
            return Ok(items);
        }

        //[HttpGet]
        //public async Task<ActionResult<Items>> GetItemsByUserName(string userName)
        //{
        //    var user = await _itemService.GetItemsByUserName(userName);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItemsById(Guid id)
        {
            var item = await _itemService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(Items newItem)
        {
            await _itemService.Create(newItem);
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Items updatedItem)
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
