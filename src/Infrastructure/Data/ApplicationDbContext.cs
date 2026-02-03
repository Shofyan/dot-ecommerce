using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category Configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        // Item Configuration
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Stock).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            // Foreign Key Configuration
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Order Configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            entity.Property(e => e.OrderDate).IsRequired();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        // OrderItem Configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            // Foreign Key Configuration for Order
            entity.HasOne(e => e.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key Configuration for Item
            entity.HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed Data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Categories (30 records)
        var categories = new List<Category>
        {
            new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Electronics", Description = "Electronic devices and gadgets", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Clothing", Description = "Fashion and apparel", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Books", Description = "Books and publications", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Home & Garden", Description = "Home improvement and garden supplies", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Sports", Description = "Sports equipment and accessories", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Toys", Description = "Toys and games for all ages", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Food & Beverages", Description = "Food items and drinks", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Health & Beauty", Description = "Health and beauty products", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Automotive", Description = "Car parts and accessories", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Office Supplies", Description = "Office and stationery items", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Pet Supplies", Description = "Products for pets", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Music Instruments", Description = "Musical instruments and accessories", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Jewelry", Description = "Jewelry and accessories", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Furniture", Description = "Home and office furniture", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Kitchen", Description = "Kitchen appliances and utensils", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("11111111-2222-3333-4444-555555555555"), Name = "Baby Products", Description = "Products for babies and toddlers", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("22222222-3333-4444-5555-666666666666"), Name = "Travel", Description = "Travel accessories and luggage", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("33333333-4444-5555-6666-777777777777"), Name = "Art & Crafts", Description = "Art supplies and craft materials", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("44444444-5555-6666-7777-888888888888"), Name = "Photography", Description = "Cameras and photography equipment", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("55555555-6666-7777-8888-999999999999"), Name = "Gaming", Description = "Video games and gaming accessories", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("66666666-7777-8888-9999-aaaaaaaaaaaa"), Name = "Outdoor", Description = "Outdoor and camping gear", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("77777777-8888-9999-aaaa-bbbbbbbbbbbb"), Name = "Watches", Description = "Watches and smartwatches", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("88888888-9999-aaaa-bbbb-cccccccccccc"), Name = "Shoes", Description = "Footwear for all occasions", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("99999999-aaaa-bbbb-cccc-dddddddddddd"), Name = "Bags", Description = "Bags and backpacks", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), Name = "Stationery", Description = "Pens, notebooks and stationery", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), Name = "Tools", Description = "Hand tools and power tools", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("cccccccc-dddd-eeee-ffff-111111111111"), Name = "Lighting", Description = "Lamps and lighting fixtures", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("dddddddd-eeee-ffff-1111-222222222222"), Name = "Cleaning", Description = "Cleaning supplies and equipment", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("eeeeeeee-ffff-1111-2222-333333333333"), Name = "Security", Description = "Security and surveillance equipment", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = Guid.Parse("ffffffff-1111-2222-3333-444444444444"), Name = "Seasonal", Description = "Seasonal and holiday items", CreatedAt = seedDate, UpdatedAt = seedDate }
        };

        modelBuilder.Entity<Category>().HasData(categories);

        // Seed Items (30 records)
        var items = new List<Item>
        {
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Smartphone Pro X", Price = 12500000m, Stock = 50, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Wireless Earbuds", Price = 1850000m, Stock = 100, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Cotton T-Shirt", Price = 185000m, Stock = 200, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Denim Jeans", Price = 450000m, Stock = 150, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Programming Guide", Price = 275000m, Stock = 75, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Garden Tool Set", Price = 650000m, Stock = 40, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Yoga Mat", Price = 225000m, Stock = 120, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), CategoryId = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Building Blocks Set", Price = 350000m, Stock = 80, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), CategoryId = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Organic Coffee Beans", Price = 175000m, Stock = 200, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), CategoryId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Skincare Set", Price = 750000m, Stock = 60, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000011"), CategoryId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Car Phone Mount", Price = 125000m, Stock = 90, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000012"), CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Desk Organizer", Price = 95000m, Stock = 110, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000013"), CategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Dog Food Premium", Price = 285000m, Stock = 70, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000014"), CategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Acoustic Guitar", Price = 2250000m, Stock = 25, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000015"), CategoryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Silver Necklace", Price = 850000m, Stock = 45, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000016"), CategoryId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Office Chair Ergonomic", Price = 3500000m, Stock = 30, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000017"), CategoryId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Blender Pro", Price = 1250000m, Stock = 55, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000018"), CategoryId = Guid.Parse("11111111-2222-3333-4444-555555555555"), Name = "Baby Stroller", Price = 2750000m, Stock = 20, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000019"), CategoryId = Guid.Parse("22222222-3333-4444-5555-666666666666"), Name = "Travel Backpack", Price = 575000m, Stock = 65, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000020"), CategoryId = Guid.Parse("33333333-4444-5555-6666-777777777777"), Name = "Acrylic Paint Set", Price = 325000m, Stock = 85, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000021"), CategoryId = Guid.Parse("44444444-5555-6666-7777-888888888888"), Name = "DSLR Camera", Price = 8500000m, Stock = 15, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000022"), CategoryId = Guid.Parse("55555555-6666-7777-8888-999999999999"), Name = "Gaming Mouse", Price = 650000m, Stock = 95, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000023"), CategoryId = Guid.Parse("66666666-7777-8888-9999-aaaaaaaaaaaa"), Name = "Camping Tent", Price = 1750000m, Stock = 35, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000024"), CategoryId = Guid.Parse("77777777-8888-9999-aaaa-bbbbbbbbbbbb"), Name = "Smart Watch", Price = 2850000m, Stock = 40, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000025"), CategoryId = Guid.Parse("88888888-9999-aaaa-bbbb-cccccccccccc"), Name = "Running Shoes", Price = 1150000m, Stock = 75, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000026"), CategoryId = Guid.Parse("99999999-aaaa-bbbb-cccc-dddddddddddd"), Name = "Leather Messenger Bag", Price = 950000m, Stock = 50, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000027"), CategoryId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), Name = "Premium Notebook Set", Price = 145000m, Stock = 130, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000028"), CategoryId = Guid.Parse("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), Name = "Power Drill Set", Price = 1450000m, Stock = 45, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000029"), CategoryId = Guid.Parse("cccccccc-dddd-eeee-ffff-111111111111"), Name = "LED Desk Lamp", Price = 375000m, Stock = 70, CreatedAt = seedDate, UpdatedAt = seedDate },
            new Item { Id = Guid.Parse("10000000-0000-0000-0000-000000000030"), CategoryId = Guid.Parse("dddddddd-eeee-ffff-1111-222222222222"), Name = "Vacuum Cleaner", Price = 2250000m, Stock = 30, CreatedAt = seedDate, UpdatedAt = seedDate }
        };

        modelBuilder.Entity<Item>().HasData(items);

        // Seed Orders (30 records)
        var orders = new List<Order>
        {
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), OrderNumber = "ORD-20260101-0001", OrderDate = seedDate, TotalAmount = 14350000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), OrderNumber = "ORD-20260101-0002", OrderDate = seedDate, TotalAmount = 635000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), OrderNumber = "ORD-20260101-0003", OrderDate = seedDate, TotalAmount = 275000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), OrderNumber = "ORD-20260101-0004", OrderDate = seedDate, TotalAmount = 875000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), OrderNumber = "ORD-20260101-0005", OrderDate = seedDate, TotalAmount = 650000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000006"), OrderNumber = "ORD-20260101-0006", OrderDate = seedDate, TotalAmount = 3700000m, Status = OrderStatus.CANCELLED, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000007"), OrderNumber = "ORD-20260101-0007", OrderDate = seedDate, TotalAmount = 350000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000008"), OrderNumber = "ORD-20260101-0008", OrderDate = seedDate, TotalAmount = 525000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000009"), OrderNumber = "ORD-20260101-0009", OrderDate = seedDate, TotalAmount = 750000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000010"), OrderNumber = "ORD-20260101-0010", OrderDate = seedDate, TotalAmount = 1850000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000011"), OrderNumber = "ORD-20260101-0011", OrderDate = seedDate, TotalAmount = 2250000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000012"), OrderNumber = "ORD-20260101-0012", OrderDate = seedDate, TotalAmount = 850000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000013"), OrderNumber = "ORD-20260101-0013", OrderDate = seedDate, TotalAmount = 3500000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000014"), OrderNumber = "ORD-20260101-0014", OrderDate = seedDate, TotalAmount = 1250000m, Status = OrderStatus.CANCELLED, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000015"), OrderNumber = "ORD-20260101-0015", OrderDate = seedDate, TotalAmount = 2750000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000016"), OrderNumber = "ORD-20260101-0016", OrderDate = seedDate, TotalAmount = 575000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000017"), OrderNumber = "ORD-20260101-0017", OrderDate = seedDate, TotalAmount = 325000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000018"), OrderNumber = "ORD-20260101-0018", OrderDate = seedDate, TotalAmount = 8500000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000019"), OrderNumber = "ORD-20260101-0019", OrderDate = seedDate, TotalAmount = 650000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000020"), OrderNumber = "ORD-20260101-0020", OrderDate = seedDate, TotalAmount = 1750000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000021"), OrderNumber = "ORD-20260101-0021", OrderDate = seedDate, TotalAmount = 2850000m, Status = OrderStatus.CANCELLED, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000022"), OrderNumber = "ORD-20260101-0022", OrderDate = seedDate, TotalAmount = 1150000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000023"), OrderNumber = "ORD-20260101-0023", OrderDate = seedDate, TotalAmount = 950000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000024"), OrderNumber = "ORD-20260101-0024", OrderDate = seedDate, TotalAmount = 145000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000025"), OrderNumber = "ORD-20260101-0025", OrderDate = seedDate, TotalAmount = 1450000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000026"), OrderNumber = "ORD-20260101-0026", OrderDate = seedDate, TotalAmount = 375000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000027"), OrderNumber = "ORD-20260101-0027", OrderDate = seedDate, TotalAmount = 2250000m, Status = OrderStatus.CANCELLED, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000028"), OrderNumber = "ORD-20260101-0028", OrderDate = seedDate, TotalAmount = 285000m, Status = OrderStatus.PAID, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000029"), OrderNumber = "ORD-20260101-0029", OrderDate = seedDate, TotalAmount = 125000m, Status = OrderStatus.PENDING, CreatedAt = seedDate },
            new Order { Id = Guid.Parse("20000000-0000-0000-0000-000000000030"), OrderNumber = "ORD-20260101-0030", OrderDate = seedDate, TotalAmount = 95000m, Status = OrderStatus.PAID, CreatedAt = seedDate }
        };

        modelBuilder.Entity<Order>().HasData(orders);

        // Seed OrderItems (linked to orders and items)
        var orderItems = new List<OrderItem>
        {
            // Order 1: Smartphone + Earbuds
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000001"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000001"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000001"), Quantity = 1, Price = 12500000m },
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000002"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000001"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000002"), Quantity = 1, Price = 1850000m },
            // Order 2: T-Shirt + Jeans
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000003"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000002"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000003"), Quantity = 1, Price = 185000m },
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000004"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000002"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000004"), Quantity = 1, Price = 450000m },
            // Order 3: Programming Book
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000005"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000003"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000005"), Quantity = 1, Price = 275000m },
            // Order 4: Garden Tools + Yoga Mat
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000006"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000004"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000006"), Quantity = 1, Price = 650000m },
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000007"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000004"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000007"), Quantity = 1, Price = 225000m },
            // Order 5: Garden Tools
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000008"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000005"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000006"), Quantity = 1, Price = 650000m },
            // Order 6: Earbuds x2 (Cancelled)
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000009"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000006"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000002"), Quantity = 2, Price = 1850000m },
            // Order 7: Building Blocks
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000010"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000007"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000008"), Quantity = 1, Price = 350000m },
            // Order 8: Coffee x3
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000011"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000008"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000009"), Quantity = 3, Price = 175000m },
            // Order 9: Skincare Set
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000012"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000009"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000010"), Quantity = 1, Price = 750000m },
            // Order 10: Earbuds
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000013"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000010"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000002"), Quantity = 1, Price = 1850000m },
            // Order 11: Acoustic Guitar
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000014"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000011"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000014"), Quantity = 1, Price = 2250000m },
            // Order 12: Silver Necklace
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000015"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000012"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000015"), Quantity = 1, Price = 850000m },
            // Order 13: Office Chair
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000016"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000013"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000016"), Quantity = 1, Price = 3500000m },
            // Order 14: Blender (Cancelled)
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000017"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000014"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000017"), Quantity = 1, Price = 1250000m },
            // Order 15: Baby Stroller
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000018"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000015"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000018"), Quantity = 1, Price = 2750000m },
            // Order 16: Travel Backpack
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000019"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000016"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000019"), Quantity = 1, Price = 575000m },
            // Order 17: Acrylic Paint Set
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000020"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000017"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000020"), Quantity = 1, Price = 325000m },
            // Order 18: DSLR Camera
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000021"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000018"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000021"), Quantity = 1, Price = 8500000m },
            // Order 19: Gaming Mouse
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000022"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000019"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000022"), Quantity = 1, Price = 650000m },
            // Order 20: Camping Tent
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000023"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000020"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000023"), Quantity = 1, Price = 1750000m },
            // Order 21: Smart Watch (Cancelled)
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000024"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000021"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000024"), Quantity = 1, Price = 2850000m },
            // Order 22: Running Shoes
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000025"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000022"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000025"), Quantity = 1, Price = 1150000m },
            // Order 23: Leather Messenger Bag
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000026"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000023"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000026"), Quantity = 1, Price = 950000m },
            // Order 24: Premium Notebook Set
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000027"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000024"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000027"), Quantity = 1, Price = 145000m },
            // Order 25: Power Drill Set
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000028"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000025"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000028"), Quantity = 1, Price = 1450000m },
            // Order 26: LED Desk Lamp
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000029"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000026"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000029"), Quantity = 1, Price = 375000m },
            // Order 27: Vacuum Cleaner (Cancelled)
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000030"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000027"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000030"), Quantity = 1, Price = 2250000m },
            // Order 28: Dog Food
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000031"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000028"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000013"), Quantity = 1, Price = 285000m },
            // Order 29: Car Phone Mount
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000032"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000029"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000011"), Quantity = 1, Price = 125000m },
            // Order 30: Desk Organizer
            new OrderItem { Id = Guid.Parse("30000000-0000-0000-0000-000000000033"), OrderId = Guid.Parse("20000000-0000-0000-0000-000000000030"), ItemId = Guid.Parse("10000000-0000-0000-0000-000000000012"), Quantity = 1, Price = 95000m }
        };

        modelBuilder.Entity<OrderItem>().HasData(orderItems);
    }
}
