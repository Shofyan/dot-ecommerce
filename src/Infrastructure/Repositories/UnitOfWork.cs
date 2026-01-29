using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ICategoryRepository? _categoryRepository;
    private IItemRepository? _itemRepository;
    private IOrderRepository? _orderRepository;
    private IOrderItemRepository? _orderItemRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICategoryRepository Categories =>
        _categoryRepository ??= new CategoryRepository(_context);

    public IItemRepository Items =>
        _itemRepository ??= new ItemRepository(_context);

    public IOrderRepository Orders =>
        _orderRepository ??= new OrderRepository(_context);

    public IOrderItemRepository OrderItems =>
        _orderItemRepository ??= new OrderItemRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
