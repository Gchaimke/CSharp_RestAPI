using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos
{
    public record ItemDto(int Id, string Name, int DisplayOrder, DateTimeOffset CreateDate);
    public record CreateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);
    public record UpdateItemDto([Required] string Name, [Range(1, 1000)] int DisplayOrder);

}