using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetByIdWithItemsAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
