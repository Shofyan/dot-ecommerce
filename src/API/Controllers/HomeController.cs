using Application.Interfaces;
using API.ViewModels;
using API.Helpers;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IOrderService _orderService;

        public HomeController(ICategoryService categoryService, IItemService itemService, IOrderService orderService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            var items = (await _itemService.GetAllItemsAsync()).ToList();
            var orders = (await _orderService.GetAllOrdersAsync()).ToList();

            var dashboard = new DashboardViewModel
            {
                TotalCategories = categories.Count,
                TotalItems = items.Count,
                TotalOrders = orders.Count,
                PendingOrders = orders.Count(o => o.Status == OrderStatus.PENDING),
                PaidOrders = orders.Count(o => o.Status == OrderStatus.PAID),
                CancelledOrders = orders.Count(o => o.Status == OrderStatus.CANCELLED),
                TotalRevenue = orders.Where(o => o.Status == OrderStatus.PAID).Sum(o => o.TotalAmount),
                TotalItemsInStock = items.Sum(i => i.Stock),
                RecentOrders = orders
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .Select(o => new RecentOrderViewModel
                    {
                        Id = o.Id,
                        OrderNumber = o.OrderNumber,
                        OrderDate = o.OrderDate,
                        TotalAmount = o.TotalAmount,
                        Status = o.Status.ToString(),
                        StatusBadgeClass = o.Status == OrderStatus.PAID ? "badge-success" :
                                          o.Status == OrderStatus.CANCELLED ? "badge-danger" : "badge-warning"
                    })
                    .ToList(),
                TopItems = items
                    .OrderByDescending(i => i.Stock)
                    .Take(5)
                    .Select(i => new TopItemViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Price = i.Price,
                        Stock = i.Stock,
                        CategoryName = i.CategoryName ?? "Uncategorized"
                    })
                    .ToList(),
                CategorySummaries = categories
                    .Select(c => new CategorySummaryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ItemCount = items.Count(i => i.CategoryId == c.Id)
                    })
                    .OrderByDescending(c => c.ItemCount)
                    .Take(5)
                    .ToList()
            };

            return View(dashboard);
        }
    }
}
