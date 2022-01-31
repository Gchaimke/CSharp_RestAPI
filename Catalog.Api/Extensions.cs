using Catalog.Api.Dtos;
using Catalog.Api.Entities;

namespace Catalog.Api
{
    public static class Extensions
    {
        public static ItemDto AsItemDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.DisplayOrder, item.CreateDateTime)
            {
                Id = item.Id,
                Name = item.Name,
                DisplayOrder = item.DisplayOrder,
                CreateDate = item.CreateDateTime
            };
        }

        public static OrderDto AsOrderDto(this Order order)
        {
            return new OrderDto(order.ORD,order.CUST, order.ORDNAME)
            {
                ORD = order.ORD,
                ORDNAME = order.ORDNAME,
                CUST = order.CUST
            };
        }
    }
}