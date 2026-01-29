# E-Commerce CRUD API

A RESTful API for managing e-commerce categories and items built with ASP.NET Core 8 and Clean Architecture.

## Architecture

This project follows Clean Architecture principles with the following layers:

- **Domain**: Core business entities and repository interfaces
- **Application**: Business logic, DTOs, and service interfaces
- **Infrastructure**: Data access implementation with Entity Framework Core
- **API**: RESTful API controllers and configuration

## Tech Stack

- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **SQLite Database**
- **Swagger/OpenAPI** for API documentation
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

### Running the Application

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

### Access Swagger UI

Navigate to: `https://localhost:5001/swagger`

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
│   │   └── Interfaces/
│   ├── Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   └── Services/
│   ├── Infrastructure/
│   │   ├── Data/
│   │   └── Repositories/
│   └── API/
│       ├── Controllers/
│       ├── Program.cs
│       └── appsettings.json
└── ecommerce.sln
```

## Database

The application uses SQLite as the database. The database file (`ecommerce.db`) will be created automatically in the API project directory on first run.

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

## License

MIT
