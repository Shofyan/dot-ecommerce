using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ItemRepository : Repository<Item>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Items
            .Where(i => i.CategoryId == categoryId)
            .Include(i => i.Category)
            .ToListAsync();
    }

    public async Task<Item?> GetByIdWithCategoryAsync(Guid id)
    {
        return await _context.Items
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
