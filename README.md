# E-Commerce CRUD API

A full-featured e-commerce application with RESTful API and MVC Web Interface built with ASP.NET Core 8 and Clean Architecture.

## Architecture

This project follows Clean Architecture principles with the following layers:

- **Domain**: Core business entities and repository interfaces
- **Application**: Business logic, DTOs, and service interfaces
- **Infrastructure**: Data access implementation with Entity Framework Core
- **API**: RESTful API controllers, MVC controllers, and Razor Views

## Tech Stack

- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **SQLite Database**
- **Swagger/OpenAPI** for API documentation
- **Bootstrap 4** for UI styling
- **Razor Views** for MVC web interface
- **Docker & Docker Compose** for containerization
- **Clean Architecture**
- **Repository Pattern**
- **Unit of Work Pattern**
- **Dependency Injection**

## Entities

### Category
- Id (GUID)
- Name
- Description
- CreatedAt
- UpdatedAt

### Item
- Id (GUID)
- CategoryId (Foreign Key)
- Name
- Price
- Stock
- CreatedAt
- UpdatedAt

### Order
- Id (GUID)
- OrderNumber (Unique, Auto-generated)
- OrderDate (UTC)
- TotalAmount
- Status (PENDING, PAID, CANCELLED)
- CreatedAt

### OrderItem
- Id (GUID)
- OrderId (Foreign Key)
- ItemId (Foreign Key)
- Quantity
- Price (at time of order)

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker & Docker Compose (optional, for containerized deployment)

### Option 1: Running with Docker (Recommended)

1. Build and run with Docker Compose:
```bash
docker-compose up -d
```

2. Access the application:
   - Web Interface: http://localhost:5000
   - Swagger API: http://localhost:5000/swagger

3. Stop the application:
```bash
docker-compose down
```

4. View logs:
```bash
docker-compose logs -f
```

5. Rebuild after code changes:
```bash
docker-compose up -d --build
```

### Option 2: Running Locally

1. Restore dependencies:
```bash
dotnet restore
```

2. Build the solution:
```bash
dotnet build
```

3. Run the API:
```bash
cd src/API
dotnet run
```

The API will start on `https://localhost:5001` (or `http://localhost:5000`)

### Access Web Interface

Navigate to: `http://localhost:5000` or `https://localhost:5001`

The web interface provides:
- **Categories Management**: Create, view, edit, and delete categories
- **Items Management**: Create, view, edit, and delete items with category selection
- **Orders Management**: Create orders, view order details, update status, and delete orders

### Access Swagger UI

Navigate to: `https://localhost:5001/swagger`

## Web Interface Routes

### Categories (MVC)
- `/Categories` - List all categories
- `/Categories/Create` - Create new category form
- `/Categories/Edit/{id}` - Edit category form
- `/Categories/Details/{id}` - View category details
- `/Categories/Delete/{id}` - Delete category confirmation

### Items (MVC)
- `/Items` - List all items
- `/Items/Create` - Create new item form
- `/Items/Edit/{id}` - Edit item form
- `/Items/Details/{id}` - View item details
- `/Items/Delete/{id}` - Delete item confirmation

### Orders (MVC)
- `/Orders` - List all orders with status badges
- `/Orders/Create` - Create new order form (JSON format)
- `/Orders/Edit/{id}` - Update order status
- `/Orders/Details/{id}` - View order details with order items
- `/Orders/Delete/{id}` - Delete order confirmation

## API Endpoints

### Categories

- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### Items

- `GET /api/items` - Get all items
- `GET /api/items/{id}` - Get item by ID
- `GET /api/items/category/{categoryId}` - Get items by category
- `POST /api/items` - Create new item
- `PUT /api/items/{id}` - Update item
- `DELETE /api/items/{id}` - Delete item

### Orders

- `GET /api/orders` - Get all orders
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders/number/{orderNumber}` - Get order by order number
- `GET /api/orders/status/{status}` - Get orders by status (PENDING/PAID/CANCELLED)
- `POST /api/orders` - Create new order
- `PATCH /api/orders/{id}/status` - Update order status
- `POST /api/orders/{id}/cancel` - Cancel order (restores stock)
- `DELETE /api/orders/{id}` - Delete order

## Example Requests

### Create Category
```json
POST /api/categories
{
  "name": "Electronics",
  "description": "Electronic devices and accessories"
}
```

### Create Item
```json
POST /api/items
{
  "categoryId": "guid-here",
  "name": "Laptop",
  "price": 999.99,
  "stock": 10
}
```

### Create Order
```json
POST /api/orders
{
  "orderItems": [
    {
      "itemId": "guid-here",
      "quantity": 2
    }
  ]
}
```

## Project Structure

```
ecommerce/
├── src/
│   ├── Domain/
│   │   ├── Entities/
│   │   │   ├── Category.cs
│   │   │   ├── Item.cs
│   │   │   ├── Order.cs
│   │   │   └── OrderItem.cs
│   │   ├── Enums/
│   │   │   └── OrderStatus.cs
│   │   └── Interfaces/
│   ├── Application/
│   │   ├── DTOs/
│   │   │   ├── CategoryDto.cs
│   │   │   ├── ItemDto.cs
│   │   │   ├── OrderDto.cs
│   │   │   └── OrderItemDto.cs
│   │   ├── Interfaces/
│   │   └── Services/
│   ├── Infrastructure/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   └── Repositories/
│   └── API/
│       ├── Controllers/
│       │   ├── CategoriesApiController.cs
│       │   ├── CategoriesController.cs
│       │   ├── ItemsApiController.cs
│       │   ├── ItemsController.cs
│       │   ├── OrdersApiController.cs
│       │   └── OrdersController.cs
│       ├── Views/
│       │   ├── Categories/
│       │   │   ├── Index.cshtml
│       │   │   ├── Create.cshtml
│       │   │   ├── Edit.cshtml
│       │   │   ├── Details.cshtml
│       │   │   └── Delete.cshtml
│       │   ├── Items/
│       │   │   ├── Index.cshtml
│       │   │   ├── Create.cshtml
│       │   │   ├── Edit.cshtml
│       │   │   ├── Details.cshtml
│       │   │   └── Delete.cshtml
│       │   ├── Orders/
│       │   │   ├── Index.cshtml
│       │   │   ├── Create.cshtml
│       │   │   ├── Edit.cshtml
│       │   │   ├── Details.cshtml
│       │   │   └── Delete.cshtml
│       │   ├── Shared/
│       │   │   ├── _Layout.cshtml
│       │   │   └── _ValidationScriptsPartial.cshtml
│       │   ├── _ViewImports.cshtml
│       │   └── _ViewStart.cshtml
│       ├── Program.cs
│       └── appsettings.json
├── Dockerfile
├── docker-compose.yml
├── .dockerignore
└── ecommerce.sln
```

## Database

The application uses SQLite as the database. The database file (`ecommerce.db`) will be created automatically in the API project directory on first run.

When running with Docker, the database is stored in a Docker volume (`ecommerce-data`) to persist data between container restarts.

## Docker Configuration

### Dockerfile
Multi-stage build for optimized image size:
- **Build stage**: Uses .NET SDK to restore, build, and publish
- **Runtime stage**: Uses lightweight ASP.NET runtime image

### Docker Compose
- Exposes application on port 5000
- Persists SQLite database in a named volume
- Configures health checks
- Auto-restarts on failure

### Docker Commands

```bash
# Build image only
docker build -t ecommerce-api .

# Run container manually
docker run -d -p 5000:80 --name ecommerce-api ecommerce-api

# View running containers
docker ps

# Stop and remove container
docker stop ecommerce-api && docker rm ecommerce-api

# Remove all data (including database)
docker-compose down -v
```

## Features

- ✅ Full CRUD operations for Categories and Items
- ✅ Order management with automatic order number generation
- ✅ Order status tracking (PENDING, PAID, CANCELLED)
- ✅ Automatic stock management (reduces on order, restores on cancel)
- ✅ Clean Architecture with separated layers
- ✅ Repository and Unit of Work patterns
- ✅ Dependency Injection
- ✅ Entity relationships (Category → Items, Order → OrderItems → Items)
- ✅ Automatic timestamps (CreatedAt, UpdatedAt)
- ✅ SQLite database with EF Core
- ✅ Swagger/OpenAPI documentation
- ✅ RESTful API design
- ✅ Error handling and validation
- ✅ Business logic (stock validation, order calculations)
- ✅ **MVC Web Interface with Razor Views**
- ✅ **Bootstrap 4 responsive UI**
- ✅ **Status badges for orders (color-coded)**
- ✅ **Currency formatting for prices**
- ✅ **Form validation with jQuery Validation**
- ✅ **Docker & Docker Compose support**
- ✅ **Multi-stage Docker build for optimized images**
- ✅ **Persistent data with Docker volumes**

## Screenshots

### Categories List
View all categories with options to create, edit, view details, or delete.

### Items List
View all items with category name, price (formatted as currency), stock quantity, and action buttons.

### Orders List
View all orders with status badges:
- 🟡 **PENDING** - Yellow badge
- 🟢 **PAID** - Green badge  
- 🔴 **CANCELLED** - Red badge

### Order Details
View order information with a detailed table of order items including item name, quantity, unit price, and subtotal.

## License

MIT
