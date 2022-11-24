using static Play.Inventory.Service.DTOs.Dtos;

namespace Play.Inventory.Service.Clients.Contracts
{
    public interface ICatalogClient
    {
        Task<IReadOnlyCollection<CatalogItemDto>> LoadCatalogItemsAsync();
    }
}
