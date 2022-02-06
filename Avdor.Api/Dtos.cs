using System.ComponentModel.DataAnnotations;
using Avdor.Api.Entities;

namespace Avdor.Api.Dtos;

public record OrderDto(string ORDNAME, string REFERENCE, long CURDATE, double TOTPRICE, IEnumerable<OrderItem> items, OrderCustomer customer, OrderShipment shipment);
public record CreateOrderDto(string REFERENCE, double TOTPRICE, long vat, IEnumerable<OrderItem> items, OrderCustomer customer, OrderShipment shipment);
public record CustomerDto(long IV, string CUSTDES, string ADDRESS, string PHONE);
public record ShipmentDto(long IV, string CUSTDES, string NAME, string ADDRESS, string CELLPHONE);
public record ItemDto(long ORD, long PART, [Range(1, 10000000)] long QUANT, double PRICE,long KLINE, double vat);

