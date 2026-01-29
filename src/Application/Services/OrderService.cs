using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _unitOfWork.Orders.GetAllAsync();
        var result = new List<OrderDto>();

        foreach (var order in orders)
        {
            var orderWithItems = await _unitOfWork.Orders.GetByIdWithItemsAsync(order.Id);
            if (orderWithItems != null)
            {
                result.Add(MapToDto(orderWithItems));
            }
        }

        return result;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
    {
        var order = await _unitOfWork.Orders.GetByIdWithItemsAsync(id);
        return order == null ? null : MapToDto(order);
    }

    public async Task<OrderDto?> GetOrderByOrderNumberAsync(string orderNumber)
    {
        var order = await _unitOfWork.Orders.GetByOrderNumberAsync(orderNumber);
        return order == null ? null : MapToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(OrderStatus status)
    {
        var orders = await _unitOfWork.Orders.GetOrdersByStatusAsync(status);
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        if (dto.OrderItems == null || !dto.OrderItems.Any())
            throw new InvalidOperationException("Order must have at least one item.");

        // Generate order number
        var orderNumber = await _unitOfWork.Orders.GenerateOrderNumberAsync();

        // Create order
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.PENDING,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 0
        };

        decimal totalAmount = 0;
        var orderItems = new List<OrderItem>();

        // Process each order item
        foreach (var itemDto in dto.OrderItems)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(itemDto.ItemId);
            if (item == null)
                throw new InvalidOperationException($"Item with ID {itemDto.ItemId} does not exist.");

            if (item.Stock < itemDto.Quantity)
                throw new InvalidOperationException($"Insufficient stock for item {item.Name}. Available: {item.Stock}, Requested: {itemDto.Quantity}");

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ItemId = item.Id,
                Quantity = itemDto.Quantity,
                Price = item.Price
            };

            totalAmount += orderItem.Price * orderItem.Quantity;
            orderItems.Add(orderItem);

            // Update stock
            item.Stock -= itemDto.Quantity;
            item.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Items.UpdateAsync(item);
        }

        order.TotalAmount = totalAmount;
        await _unitOfWork.Orders.AddAsync(order);

        // Add order items
        foreach (var orderItem in orderItems)
        {
            await _unitOfWork.OrderItems.AddAsync(orderItem);
        }

        await _unitOfWork.SaveChangesAsync();

        return MapToDto(await _unitOfWork.Orders.GetByIdWithItemsAsync(order.Id) ?? order);
    }

    public async Task<OrderDto?> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto dto)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (order == null)
            return null;

        // If cancelling, restore stock
        if (dto.Status == OrderStatus.CANCELLED && order.Status != OrderStatus.CANCELLED)
        {
            var orderItems = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);
            foreach (var orderItem in orderItems)
            {
                var item = await _unitOfWork.Items.GetByIdAsync(orderItem.ItemId);
                if (item != null)
                {
                    item.Stock += orderItem.Quantity;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Items.UpdateAsync(item);
                }
            }
        }

        order.Status = dto.Status;
        await _unitOfWork.Orders.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return await GetOrderByIdAsync(order.Id);
    }

    public async Task<bool> CancelOrderAsync(Guid id)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (order == null)
            return false;

        if (order.Status == OrderStatus.CANCELLED)
            return false;

        await UpdateOrderStatusAsync(id, new UpdateOrderStatusDto { Status = OrderStatus.CANCELLED });
        return true;
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var exists = await _unitOfWork.Orders.ExistsAsync(id);
        if (!exists)
            return false;

        await _unitOfWork.Orders.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ItemId = oi.ItemId,
                ItemName = oi.Item?.Name ?? "",
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList()
        };
    }
}
