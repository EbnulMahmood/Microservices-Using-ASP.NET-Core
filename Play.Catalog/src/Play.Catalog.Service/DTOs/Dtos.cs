using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.DTOs
{
    public sealed record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
    public sealed record CreateItemDto([Required] string Name, string Description, [Range(0, 10000000)] decimal Price);
    public sealed record UpdateItemDto([Required] string Name, string Description, [Range(0, 10000000)] decimal Price);
}