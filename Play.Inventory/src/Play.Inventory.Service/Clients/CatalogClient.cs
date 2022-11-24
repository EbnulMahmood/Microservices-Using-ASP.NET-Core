using Play.Inventory.Service.Clients.Contracts;
using static Play.Inventory.Service.DTOs.Dtos;

namespace Play.Inventory.Service.Clients
{
    public sealed class CatalogClient : ICatalogClient
    {
        private readonly HttpClient _httpClient;
        private const string _catalogItemsUrl = "/api/item";

        public CatalogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatalogItemDto>> LoadCatalogItemsAsync()
        {
            try
            {
                var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>(_catalogItemsUrl);

                return items;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
