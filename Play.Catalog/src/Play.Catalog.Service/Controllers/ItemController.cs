﻿using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Extensions;
using Play.Common.Service.IRepositories;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IRepository<Item> _itemRepository;

        public ItemController(ILogger<ItemController> logger, IRepository<Item> itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> LoadItemsAsync()
        {
            try
            {
                var items = (await _itemRepository.LoadAsync())
                    .Select(item => item.AsDto());

                return items;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
        {
            try
            {
                var item = await _itemRepository.GetByIdAsync(id);
                if (item is null) return NotFound();

                return item.AsDto();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
        {
            try
            {
                var item = new Item
                {
                    Name = createItemDto.Name,
                    Description = createItemDto.Description,
                    Price = createItemDto.Price,
                    CreatedDate = DateTimeOffset.UtcNow,
                };

                await _itemRepository.CreateAsync(item);

                return CreatedAtAction(nameof(GetItemByIdAsync), new { id = item.Id }, item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            try
            {
                var existingItem = await _itemRepository.GetByIdAsync(id);
                if (existingItem is null) return NotFound();

                existingItem.Name = updateItemDto.Name;
                existingItem.Description = updateItemDto.Description;
                existingItem.Price = updateItemDto.Price;

                await _itemRepository.UpdateAsync(existingItem);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            try
            {
                var item = await _itemRepository.GetByIdAsync(id);
                if (item is null) return NotFound();
                 
                await _itemRepository.DeleteAsync(item.Id);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
