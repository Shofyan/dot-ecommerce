using Application.DTOs;

namespace Application.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(Guid id);
    Task<IEnumerable<ItemDto>> GetItemsByCategoryAsync(Guid categoryId);
    Task<ItemDto> CreateItemAsync(CreateItemDto dto);
    Task<ItemDto?> UpdateItemAsync(Guid id, UpdateItemDto dto);
    Task<bool> DeleteItemAsync(Guid id);
}
