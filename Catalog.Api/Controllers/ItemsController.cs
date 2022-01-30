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
        private readonly SqlDbRepository repository;
        private readonly ILogger<ItemsController> logger;
        public ItemsController(SqlDbRepository repository, ILogger<ItemsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            IEnumerable<Item> items = repository.Items;
            // logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {items.Count()} items");
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            var item = await repository.Items.FindAsync(id);
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
            await repository.Items.AddAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(int id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.Items.FindAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            existingItem.Name = itemDto.Name;
            existingItem.DisplayOrder = itemDto.DisplayOrder;

            repository.Items.Update(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            var existingItem = await repository.Items.FindAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            repository.Items.Remove(existingItem);
            return NoContent();
        }
    }
}