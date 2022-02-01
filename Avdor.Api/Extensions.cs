using Avdor.Api.Dtos;
using Avdor.Api.Entities;

namespace Avdor.Api
{
    public static class Extensions
    {

        public static OrderDto AsOrderDto(this Order order)
        {
            return new OrderDto(order.ORD, order.CUST, order.ORDNAME, order.CURDATE, order.QPRICE, order.VAT, order.TOTPRICE, order.CURRENCY, order.DISPRICE, order.PAY)
            {
                ORD = order.ORD,
                ORDNAME = order.ORDNAME,
                CUST = order.CUST,
                CURDATE = order.CURDATE,
                QPRICE = order.QPRICE,
                VAT = order.VAT,
                TOTPRICE = order.TOTPRICE,
                CURRENCY = order.CURRENCY,
                DISPRICE = order.DISPRICE,
                PAY = order.PAY
            };
        }
    }
}