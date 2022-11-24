namespace Play.Inventory.Service.DTOs
{
    public sealed class Dtos
    {
        public record GrantItemsDto(Guid UserId, Guid CatalogItemId, int Quantity);
        public record InventoryItemDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);
        public sealed record CatalogItemDto(Guid Id, string Name, string Description);
    }
}
