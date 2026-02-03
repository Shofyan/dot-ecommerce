using Application.DTOs;
using Domain.Enums;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(Guid id);
    Task<OrderDto?> GetOrderByOrderNumberAsync(string orderNumber);
    Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(OrderStatus status);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto?> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto dto);
    Task<OrderDto?> UpdateOrderAsync(Guid id, UpdateOrderDto dto);
    Task<bool> CancelOrderAsync(Guid id);
    Task<bool> DeleteOrderAsync(Guid id);
}
