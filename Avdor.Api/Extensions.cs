using Avdor.Api.Dtos;
using Avdor.Api.Entities;

namespace Avdor.Api
{
    public static class Extensions
    {

        public static OrderDto AsOrderDto(this Order order)
        {
            return new OrderDto(order.ORDNAME, order.REFERENCE, order.CURDATE, order.TOTPRICE, order.items, order.customer, order.shipment)
            {

                ORDNAME = order.ORDNAME,
                REFERENCE = order.REFERENCE,
                CURDATE = order.CURDATE,
                TOTPRICE = order.TOTPRICE,
                items = order.items,
                customer = order.customer,
                shipment = order.shipment,
            };
        }

        public static CustomerDto AsCustomerDto(this OrderCustomer customer)
        {
            return new CustomerDto(customer.IV, customer.CUSTDES, customer.PHONE, customer.ADDRESS)
            {

                IV = customer.IV,
                CUSTDES = customer.CUSTDES,
                PHONE = customer.PHONE,
                ADDRESS = customer.ADDRESS,
            };
        }

        public static ShipmentDto AsShipmentDto(this OrderShipment shipment)
        {
            return new ShipmentDto(shipment.IV, shipment.CUSTDES, shipment.NAME, shipment.ADDRESS, shipment.CELLPHONE)
            {
                IV = shipment.IV,
                CUSTDES = shipment.CUSTDES,
                NAME = shipment.NAME,
                CELLPHONE = shipment.CELLPHONE,
                ADDRESS = shipment.ADDRESS,
            };
        }

        public static ItemDto AsItemDto(this OrderItem item)
        {
            return new ItemDto(item.ORD, item.PART, item.QUANT, item.PRICE,item.KLINE, item.vat)
            {
                ORD = item.ORD,
                PART = item.PART,
                QUANT = item.QUANT,
                PRICE = item.PRICE,
                KLINE = item.KLINE,
                vat = item.vat,
            };
        }
    }
}