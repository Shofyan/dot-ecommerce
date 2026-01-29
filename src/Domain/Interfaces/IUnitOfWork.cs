namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    IItemRepository Items { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    Task<int> SaveChangesAsync();
}
