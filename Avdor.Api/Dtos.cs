using System.ComponentModel.DataAnnotations;
using Avdor.Api.Entities;

namespace Avdor.Api.Dtos;

public record UpdateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);

public record OrderDto(string ORDNAME, string REFERENCE, long CURDATE, double TOTPRICE, IEnumerable<OrderItem> items);
public record CreateOrderDto(long CURDATE, string REFERENCE, double TOTPRICE, double vat, IEnumerable<OrderItem> items, OrderCustomer customer, OrderShipment shipment);
public record UpdateOrderDto(string REFERENCE, double TOTPRICE, double vat);
