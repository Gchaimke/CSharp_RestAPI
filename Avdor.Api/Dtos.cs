using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Dtos;

public record ItemDto(int Id, string Name, int DisplayOrder, DateTimeOffset CreateDate);
public record CreateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);
public record UpdateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);

public record OrderDto(long ORD, long CUST, string ORDNAME);
public record CreateOrderDto([Required] long CUST, string ORDNAME);
public record UpdateOrderDto([Required] long CUST, string ORDNAME);
