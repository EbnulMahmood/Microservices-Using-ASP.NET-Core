using Microsoft.AspNetCore.Mvc;
using Play.Common.Service.IRepositories;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extensions;
using static Play.Inventory.Service.DTOs.Dtos;

namespace Play.Inventory.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IRepository<InventoryItem> _itemRepository;

        public ItemController(ILogger<ItemController> logger, IRepository<InventoryItem> itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> LoadInventoryItemByUserIdAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty) return BadRequest();

                var items = (await _itemRepository.LoadAsync(item => item.UserId == userId))
                    .Select(item => item.AsDto());

                return Ok(items);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateInventoryItemAsync(GrantItemsDto grantItemsDto)
        {
            try
            {
                var inventoryItem = await _itemRepository
                    .GetByIdAsync(item => item.UserId == grantItemsDto.UserId &&
                        item.CatalogItemId == grantItemsDto.CatalogItemId);

                if (inventoryItem is null)
                {
                    inventoryItem = new InventoryItem
                    {
                        CatalogItemId = grantItemsDto.CatalogItemId,
                        UserId = grantItemsDto.UserId,
                        Quantity = grantItemsDto.Quantity,
                        AcquiredDate = DateTimeOffset.UtcNow
                    };

                    await _itemRepository.CreateAsync(inventoryItem);
                }
                else
                {
                    inventoryItem.Quantity += grantItemsDto.Quantity;
                    await _itemRepository.UpdateAsync(inventoryItem);
                }

                return Ok(inventoryItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
