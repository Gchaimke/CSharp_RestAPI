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

        [Route("filter")]
        public IEnumerable<Item> GetItems(string? name = "", int? order = 0)
        {
            IQueryable<Item> items = db.Items;
            if (!string.IsNullOrWhiteSpace(name))
                items = items.Where(i => i.Name.Contains(name));
            if (order > 0)
                items = items.Where(i => i.DisplayOrder >= order);
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {items.Count()} items");
            return items;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {item}");
            return item.AsItemDto();
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
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Cerated {item}");
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsItemDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(int id, UpdateItemDto itemDto)
        {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return NotFound();
            }

            item.Name = itemDto.Name;
            item.DisplayOrder = itemDto.DisplayOrder;

            db.Items.Update(item);
            await db.SaveChangesAsync();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated {item}");
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsItemDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Deleted {item}");
            return Ok("Deleted");
        }
    }
}