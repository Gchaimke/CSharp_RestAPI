using Avdor.Api.Dtos;
using Avdor.Api.Entities;

namespace Avdor.Api
{
    public static class Extensions
    {

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