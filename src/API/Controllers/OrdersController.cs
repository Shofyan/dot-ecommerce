using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using API.Helpers;

namespace API.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        private const int PageSize = 10;

        public OrdersController(IOrderService orderService, IItemService itemService)
        {
            _orderService = orderService;
            _itemService = itemService;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int page = 1)
        {
            var allOrders = (await _orderService.GetAllOrdersAsync()).ToList();
            var totalCount = allOrders.Count;
            var orders = allOrders.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var pagedResult = new PagedResult<OrderDto>
            {
                Items = orders,
                PageNumber = page,
                PageSize = PageSize,
                TotalCount = totalCount
            };

            return View(pagedResult);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var items = await _itemService.GetAllItemsAsync();
            ViewBag.Items = items.Where(i => i.Stock > 0).Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
            }).ToList();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Guid> ItemIds, List<int> Quantities)
        {
            if (ItemIds == null || ItemIds.Count == 0 || Quantities == null || Quantities.Count == 0)
            {
                ModelState.AddModelError("", "Please add at least one item to the order.");
                var items = await _itemService.GetAllItemsAsync();
                ViewBag.Items = items.Where(i => i.Stock > 0).Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
                }).ToList();
                return View();
            }

            try
            {
                var orderItems = new List<CreateOrderItemDto>();
                for (int i = 0; i < ItemIds.Count; i++)
                {
                    if (Quantities[i] > 0)
                    {
                        orderItems.Add(new CreateOrderItemDto
                        {
                            ItemId = ItemIds[i],
                            Quantity = Quantities[i]
                        });
                    }
                }

                if (orderItems.Count == 0)
                {
                    ModelState.AddModelError("", "Please add at least one item with quantity greater than 0.");
                    var items = await _itemService.GetAllItemsAsync();
                    ViewBag.Items = items.Where(i => i.Stock > 0).Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
                    }).ToList();
                    return View();
                }

                var orderDto = new CreateOrderDto { OrderItems = orderItems };
                await _orderService.CreateOrderAsync(orderDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var items = await _itemService.GetAllItemsAsync();
                ViewBag.Items = items.Where(i => i.Stock > 0).Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
                }).ToList();
                return View();
            }
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var items = await _itemService.GetAllItemsAsync();
            ViewBag.Items = items.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
            }).ToList();
            ViewBag.OrderId = id;

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderStatus Status, List<Guid> OrderItemIds, List<Guid> ItemIds, List<int> Quantities)
        {
            try
            {
                var orderItems = new List<UpdateOrderItemDto>();

                if (ItemIds != null && Quantities != null)
                {
                    for (int i = 0; i < ItemIds.Count; i++)
                    {
                        if (Quantities[i] > 0)
                        {
                            orderItems.Add(new UpdateOrderItemDto
                            {
                                Id = (OrderItemIds != null && i < OrderItemIds.Count && OrderItemIds[i] != Guid.Empty)
                                    ? OrderItemIds[i]
                                    : null,
                                ItemId = ItemIds[i],
                                Quantity = Quantities[i]
                            });
                        }
                    }
                }

                if (orderItems.Count == 0 && Status != Domain.Enums.OrderStatus.CANCELLED)
                {
                    ModelState.AddModelError("", "Order must have at least one item.");
                    var order = await _orderService.GetOrderByIdAsync(id);
                    var items = await _itemService.GetAllItemsAsync();
                    ViewBag.Items = items.Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
                    }).ToList();
                    ViewBag.OrderId = id;
                    return View(order);
                }

                var updateDto = new UpdateOrderDto
                {
                    Status = Status,
                    OrderItems = orderItems
                };

                await _orderService.UpdateOrderAsync(id, updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var order = await _orderService.GetOrderByIdAsync(id);
                var items = await _itemService.GetAllItemsAsync();
                ViewBag.Items = items.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Name} - {i.Price.ToRupiah()} (Stock: {i.Stock})"
                }).ToList();
                ViewBag.OrderId = id;
                return View(order);
            }
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
