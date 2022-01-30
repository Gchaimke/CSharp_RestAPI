using Catalog.Api.Dtos;
using Catalog.Api.Entities;

namespace Catalog.Api
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.DisplayOrder, item.CreateDateTime)
            {
                Id = item.Id,
                Name = item.Name,
                DisplayOrder = item.DisplayOrder,
                CreateDateTime = item.CreateDateTime
            };
        }
    }
}