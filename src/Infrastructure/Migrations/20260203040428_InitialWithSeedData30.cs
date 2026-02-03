using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithSeedData30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Electronic devices and gadgets", "Electronics", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Products for babies and toddlers", "Baby Products", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fashion and apparel", "Clothing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-3333-4444-5555-666666666666"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Travel accessories and luggage", "Travel", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Books and publications", "Books", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-4444-5555-6666-777777777777"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Art supplies and craft materials", "Art & Crafts", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Home improvement and garden supplies", "Home & Garden", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-5555-6666-7777-888888888888"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cameras and photography equipment", "Photography", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sports equipment and accessories", "Sports", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-6666-7777-8888-999999999999"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Video games and gaming accessories", "Gaming", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Toys and games for all ages", "Toys", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Outdoor and camping gear", "Outdoor", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Food items and drinks", "Food & Beverages", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbb"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Watches and smartwatches", "Watches", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Health and beauty products", "Health & Beauty", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("88888888-9999-aaaa-bbbb-cccccccccccc"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Footwear for all occasions", "Shoes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Car parts and accessories", "Automotive", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bags and backpacks", "Bags", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Office and stationery items", "Office Supplies", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pens, notebooks and stationery", "Stationery", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Products for pets", "Pet Supplies", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hand tools and power tools", "Tools", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Musical instruments and accessories", "Music Instruments", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cccccccc-dddd-eeee-ffff-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Lamps and lighting fixtures", "Lighting", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Jewelry and accessories", "Jewelry", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dddddddd-eeee-ffff-1111-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cleaning supplies and equipment", "Cleaning", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Home and office furniture", "Furniture", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("eeeeeeee-ffff-1111-2222-333333333333"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Security and surveillance equipment", "Security", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ffffffff-1111-2222-3333-444444444444"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seasonal and holiday items", "Seasonal", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kitchen appliances and utensils", "Kitchen", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "OrderDate", "OrderNumber", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0001", 1, 14350000m },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0002", 1, 635000m },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0003", 0, 275000m },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0004", 0, 875000m },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0005", 1, 650000m },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0006", 2, 3700000m },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0007", 0, 350000m },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0008", 1, 525000m },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0009", 0, 750000m },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0010", 1, 1850000m },
                    { new Guid("20000000-0000-0000-0000-000000000011"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0011", 1, 2250000m },
                    { new Guid("20000000-0000-0000-0000-000000000012"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0012", 0, 850000m },
                    { new Guid("20000000-0000-0000-0000-000000000013"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0013", 1, 3500000m },
                    { new Guid("20000000-0000-0000-0000-000000000014"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0014", 2, 1250000m },
                    { new Guid("20000000-0000-0000-0000-000000000015"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0015", 0, 2750000m },
                    { new Guid("20000000-0000-0000-0000-000000000016"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0016", 1, 575000m },
                    { new Guid("20000000-0000-0000-0000-000000000017"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0017", 1, 325000m },
                    { new Guid("20000000-0000-0000-0000-000000000018"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0018", 0, 8500000m },
                    { new Guid("20000000-0000-0000-0000-000000000019"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0019", 1, 650000m },
                    { new Guid("20000000-0000-0000-0000-000000000020"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0020", 1, 1750000m },
                    { new Guid("20000000-0000-0000-0000-000000000021"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0021", 2, 2850000m },
                    { new Guid("20000000-0000-0000-0000-000000000022"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0022", 0, 1150000m },
                    { new Guid("20000000-0000-0000-0000-000000000023"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0023", 1, 950000m },
                    { new Guid("20000000-0000-0000-0000-000000000024"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0024", 1, 145000m },
                    { new Guid("20000000-0000-0000-0000-000000000025"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0025", 0, 1450000m },
                    { new Guid("20000000-0000-0000-0000-000000000026"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0026", 1, 375000m },
                    { new Guid("20000000-0000-0000-0000-000000000027"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0027", 2, 2250000m },
                    { new Guid("20000000-0000-0000-0000-000000000028"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0028", 1, 285000m },
                    { new Guid("20000000-0000-0000-0000-000000000029"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0029", 0, 125000m },
                    { new Guid("20000000-0000-0000-0000-000000000030"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ORD-20260101-0030", 1, 95000m }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphone Pro X", 12500000m, 50, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless Earbuds", 1850000m, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cotton T-Shirt", 185000m, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Denim Jeans", 450000m, 150, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Programming Guide", 275000m, 75, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Garden Tool Set", 650000m, 40, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yoga Mat", 225000m, 120, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Building Blocks Set", 350000m, 80, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Organic Coffee Beans", 175000m, 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Skincare Set", 750000m, 60, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Car Phone Mount", 125000m, 90, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Desk Organizer", 95000m, 110, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dog Food Premium", 285000m, 70, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Acoustic Guitar", 2250000m, 25, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Silver Necklace", 850000m, 45, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000016"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Office Chair Ergonomic", 3500000m, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000017"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Blender Pro", 1250000m, 55, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000018"), new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Baby Stroller", 2750000m, 20, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000019"), new Guid("22222222-3333-4444-5555-666666666666"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Travel Backpack", 575000m, 65, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000020"), new Guid("33333333-4444-5555-6666-777777777777"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Acrylic Paint Set", 325000m, 85, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000021"), new Guid("44444444-5555-6666-7777-888888888888"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DSLR Camera", 8500000m, 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000022"), new Guid("55555555-6666-7777-8888-999999999999"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gaming Mouse", 650000m, 95, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000023"), new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Camping Tent", 1750000m, 35, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000024"), new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbb"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Smart Watch", 2850000m, 40, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000025"), new Guid("88888888-9999-aaaa-bbbb-cccccccccccc"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Running Shoes", 1150000m, 75, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000026"), new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Leather Messenger Bag", 950000m, 50, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000027"), new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Premium Notebook Set", 145000m, 130, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000028"), new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Power Drill Set", 1450000m, 45, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000029"), new Guid("cccccccc-dddd-eeee-ffff-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "LED Desk Lamp", 375000m, 70, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000030"), new Guid("dddddddd-eeee-ffff-1111-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vacuum Cleaner", 2250000m, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "ItemId", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001"), 12500000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000001"), 1850000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000002"), 185000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000002"), 450000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000003"), 275000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000004"), 650000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000007"), new Guid("20000000-0000-0000-0000-000000000004"), 225000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000005"), 650000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000006"), 1850000m, 2 },
                    { new Guid("30000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000008"), new Guid("20000000-0000-0000-0000-000000000007"), 350000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000011"), new Guid("10000000-0000-0000-0000-000000000009"), new Guid("20000000-0000-0000-0000-000000000008"), 175000m, 3 },
                    { new Guid("30000000-0000-0000-0000-000000000012"), new Guid("10000000-0000-0000-0000-000000000010"), new Guid("20000000-0000-0000-0000-000000000009"), 750000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000013"), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000010"), 1850000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000014"), new Guid("10000000-0000-0000-0000-000000000014"), new Guid("20000000-0000-0000-0000-000000000011"), 2250000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000015"), new Guid("10000000-0000-0000-0000-000000000015"), new Guid("20000000-0000-0000-0000-000000000012"), 850000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000016"), new Guid("10000000-0000-0000-0000-000000000016"), new Guid("20000000-0000-0000-0000-000000000013"), 3500000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000017"), new Guid("10000000-0000-0000-0000-000000000017"), new Guid("20000000-0000-0000-0000-000000000014"), 1250000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000018"), new Guid("10000000-0000-0000-0000-000000000018"), new Guid("20000000-0000-0000-0000-000000000015"), 2750000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000019"), new Guid("10000000-0000-0000-0000-000000000019"), new Guid("20000000-0000-0000-0000-000000000016"), 575000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000020"), new Guid("10000000-0000-0000-0000-000000000020"), new Guid("20000000-0000-0000-0000-000000000017"), 325000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000021"), new Guid("10000000-0000-0000-0000-000000000021"), new Guid("20000000-0000-0000-0000-000000000018"), 8500000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000022"), new Guid("10000000-0000-0000-0000-000000000022"), new Guid("20000000-0000-0000-0000-000000000019"), 650000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000023"), new Guid("10000000-0000-0000-0000-000000000023"), new Guid("20000000-0000-0000-0000-000000000020"), 1750000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000024"), new Guid("10000000-0000-0000-0000-000000000024"), new Guid("20000000-0000-0000-0000-000000000021"), 2850000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000025"), new Guid("10000000-0000-0000-0000-000000000025"), new Guid("20000000-0000-0000-0000-000000000022"), 1150000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000026"), new Guid("10000000-0000-0000-0000-000000000026"), new Guid("20000000-0000-0000-0000-000000000023"), 950000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000027"), new Guid("10000000-0000-0000-0000-000000000027"), new Guid("20000000-0000-0000-0000-000000000024"), 145000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000028"), new Guid("10000000-0000-0000-0000-000000000028"), new Guid("20000000-0000-0000-0000-000000000025"), 1450000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000029"), new Guid("10000000-0000-0000-0000-000000000029"), new Guid("20000000-0000-0000-0000-000000000026"), 375000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000030"), new Guid("10000000-0000-0000-0000-000000000030"), new Guid("20000000-0000-0000-0000-000000000027"), 2250000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000031"), new Guid("10000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000028"), 285000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000032"), new Guid("10000000-0000-0000-0000-000000000011"), new Guid("20000000-0000-0000-0000-000000000029"), 125000m, 1 },
                    { new Guid("30000000-0000-0000-0000-000000000033"), new Guid("10000000-0000-0000-0000-000000000012"), new Guid("20000000-0000-0000-0000-000000000030"), 95000m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
