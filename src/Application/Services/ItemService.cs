using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
    {
        var items = await _unitOfWork.Items.GetAllAsync();
        return items.Select(i => new ItemDto
        {
            Id = i.Id,
            CategoryId = i.CategoryId,
            Name = i.Name,
            Price = i.Price,
            Stock = i.Stock,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt,
            CategoryName = i.Category?.Name
        });
    }

    public async Task<ItemDto?> GetItemByIdAsync(Guid id)
    {
        var item = await _unitOfWork.Items.GetByIdWithCategoryAsync(id);
        if (item == null)
            return null;

        return new ItemDto
        {
            Id = item.Id,
            CategoryId = item.CategoryId,
            Name = item.Name,
            Price = item.Price,
            Stock = item.Stock,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            CategoryName = item.Category?.Name
        };
    }

    public async Task<IEnumerable<ItemDto>> GetItemsByCategoryAsync(Guid categoryId)
    {
        var items = await _unitOfWork.Items.GetByCategoryIdAsync(categoryId);
        return items.Select(i => new ItemDto
        {
            Id = i.Id,
            CategoryId = i.CategoryId,
            Name = i.Name,
            Price = i.Price,
            Stock = i.Stock,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt,
            CategoryName = i.Category?.Name
        });
    }

    public async Task<ItemDto> CreateItemAsync(CreateItemDto dto)
    {
        // Verify category exists
        var categoryExists = await _unitOfWork.Categories.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new InvalidOperationException($"Category with ID {dto.CategoryId} does not exist.");

        var item = new Item
        {
            Id = Guid.NewGuid(),
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Items.AddAsync(item);
        await _unitOfWork.SaveChangesAsync();

        var createdItem = await _unitOfWork.Items.GetByIdWithCategoryAsync(item.Id);
        return new ItemDto
        {
            Id = createdItem!.Id,
            CategoryId = createdItem.CategoryId,
            Name = createdItem.Name,
            Price = createdItem.Price,
            Stock = createdItem.Stock,
            CreatedAt = createdItem.CreatedAt,
            UpdatedAt = createdItem.UpdatedAt,
            CategoryName = createdItem.Category?.Name
        };
    }

    public async Task<ItemDto?> UpdateItemAsync(Guid id, UpdateItemDto dto)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id);
        if (item == null)
            return null;

        // Verify category exists if it's being changed
        if (item.CategoryId != dto.CategoryId)
        {
            var categoryExists = await _unitOfWork.Categories.ExistsAsync(dto.CategoryId);
            if (!categoryExists)
                throw new InvalidOperationException($"Category with ID {dto.CategoryId} does not exist.");
        }

        item.CategoryId = dto.CategoryId;
        item.Name = dto.Name;
        item.Price = dto.Price;
        item.Stock = dto.Stock;
        item.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync();

        var updatedItem = await _unitOfWork.Items.GetByIdWithCategoryAsync(item.Id);
        return new ItemDto
        {
            Id = updatedItem!.Id,
            CategoryId = updatedItem.CategoryId,
            Name = updatedItem.Name,
            Price = updatedItem.Price,
            Stock = updatedItem.Stock,
            CreatedAt = updatedItem.CreatedAt,
            UpdatedAt = updatedItem.UpdatedAt,
            CategoryName = updatedItem.Category?.Name
        };
    }

    public async Task<bool> DeleteItemAsync(Guid id)
    {
        var exists = await _unitOfWork.Items.ExistsAsync(id);
        if (!exists)
            return false;

        await _unitOfWork.Items.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
