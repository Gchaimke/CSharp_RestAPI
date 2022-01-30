using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = 1, Name = "Poition", DisplayOrder = 1, CreateDateTime = DateTime.UtcNow },
            new Item { Id = 2, Name = "Iron Sword", DisplayOrder = 2, CreateDateTime = DateTime.UtcNow },
            new Item { Id = 3, Name = "Bronze Shield", DisplayOrder = 3, CreateDateTime = DateTime.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            var item =  items.SingleOrDefault(item => item.Id == id);
            return await Task.FromResult(item);
        }
 
        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemASync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }
 
        public async Task DeleteItemAsync(int id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}