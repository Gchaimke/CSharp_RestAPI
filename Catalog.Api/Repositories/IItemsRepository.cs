using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IItemsRepository
    {
        
        Task<Item> GetItemAsync(int id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemASync(Item item);
        Task DeleteItemAsync(int id);
    }
    
}