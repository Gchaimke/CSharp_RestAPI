using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Dtos;

public record UpdateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);

public record OrderDto(string ORDNAME, string REFERENCE, long CURDATE, double TOTPRICE);
public record CreateOrderDto( long CURDATE, string REFERENCE, double TOTPRICE, double vat);

public record UpdateOrderDto(string REFERENCE, double TOTPRICE, double vat);
