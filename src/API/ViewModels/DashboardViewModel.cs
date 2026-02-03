namespace API.ViewModels;

public class DashboardViewModel
{
    public int TotalCategories { get; set; }
    public int TotalItems { get; set; }
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int PaidOrders { get; set; }
    public int CancelledOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalItemsInStock { get; set; }
    public List<RecentOrderViewModel> RecentOrders { get; set; } = new();
    public List<TopItemViewModel> TopItems { get; set; } = new();
    public List<CategorySummaryViewModel> CategorySummaries { get; set; } = new();
}

public class RecentOrderViewModel
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string StatusBadgeClass { get; set; } = string.Empty;
}

public class TopItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class CategorySummaryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ItemCount { get; set; }
}
