using Domain.Entities;

namespace Domain.Interfaces;

public interface IItemRepository : IRepository<Item>
{
    Task<IEnumerable<Item>> GetByCategoryIdAsync(Guid categoryId);
    Task<Item?> GetByIdWithCategoryAsync(Guid id);
}
