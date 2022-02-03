using Avdor.Api.Dtos;
using Avdor.Api.Entities;

namespace Avdor.Api
{
    public static class Extensions
    {

        public static OrderDto AsOrderDto(this Order order)
        {
            return new OrderDto(order.ORDNAME, order.REFERENCE, order.CURDATE, order.TOTPRICE, order.items)
            {
                ORDNAME = order.ORDNAME,
                REFERENCE = order.REFERENCE,
                CURDATE = order.CURDATE,
                TOTPRICE = order.TOTPRICE,
            };
        }
    }
}