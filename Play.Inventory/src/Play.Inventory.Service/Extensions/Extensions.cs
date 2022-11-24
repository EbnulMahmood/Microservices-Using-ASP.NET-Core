using Play.Inventory.Service.Entities;
using static Play.Inventory.Service.DTOs.Dtos;

namespace Play.Inventory.Service.Extensions
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item)
        {
            return new InventoryItemDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
        }
    }
}
