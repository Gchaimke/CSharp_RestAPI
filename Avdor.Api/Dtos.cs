using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Dtos;

public record UpdateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);

public record OrderDto(long ORD, long CUST, string ORDNAME, long CURDATE, double QPRICE, double VAT, double TOTPRICE, long CURRENCY, double DISPRICE, long PAY);
public record CreateOrderDto([Required] long CUST, long CURDATE, double QPRICE, long CURRENCY);
public record UpdateOrderDto([Required] long CUST, long CURDATE, double QPRICE, long CURRENCY);
