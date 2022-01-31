using Microsoft.AspNetCore.Mvc;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly SqlDbRepository db;
        private readonly ILogger<ItemsController> logger;
        public ItemsController(SqlDbRepository db, ILogger<ItemsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            IEnumerable<Item> items = db.Items;
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {items.Count()} items");
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Name = itemDto.Name,
                DisplayOrder = itemDto.DisplayOrder,
            };
            await db.Items.AddAsync(item);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(int id, UpdateItemDto itemDto)
        {
            var existingItem = await db.Items.FindAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            existingItem.Name = itemDto.Name;
            existingItem.DisplayOrder = itemDto.DisplayOrder;

            db.Items.Update(existingItem);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemAsync), new { id = existingItem.Id }, existingItem.AsDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            var existingItem = await db.Items.FindAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            db.Items.Remove(existingItem);
            await db.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}