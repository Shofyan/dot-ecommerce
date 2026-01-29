# Order System - Complete Guide

## Overview

The Order system allows you to create orders with multiple items, automatically:
- Generates unique order numbers (format: ORD{YYYYMMDD}{sequence})
- Calculates total amount
- Reduces item stock
- Tracks order status (PENDING, PAID, CANCELLED)
- Restores stock when orders are cancelled

## Order Status

- **PENDING** (0): Order created, awaiting payment
- **PAID** (1): Payment received, order confirmed
- **CANCELLED** (2): Order cancelled, stock restored

## Entities & Relationships

### Order
- `id` (GUID): Unique identifier
- `orderNumber` (string): Auto-generated unique number
- `orderDate` (DateTime): UTC timestamp
- `totalAmount` (decimal): Calculated from order items
- `status` (OrderStatus): PENDING, PAID, or CANCELLED
- `createdAt` (DateTime): Creation timestamp

### OrderItem
- `id` (GUID): Unique identifier
- `orderId` (FK): Reference to Order
- `itemId` (FK): Reference to Item
- `quantity` (int): Number of items ordered
- `price` (decimal): Item price at time of order

## API Endpoints

### GET /api/orders
Get all orders with their items

**Response:**
```json
[
  {
    "id": "guid",
    "orderNumber": "ORD202601280001",
    "orderDate": "2026-01-28T14:51:57.733409Z",
    "totalAmount": 1999.98,
    "status": 0,
    "createdAt": "2026-01-28T14:51:57.733427Z",
    "orderItems": [
      {
        "id": "guid",
        "orderId": "guid",
        "itemId": "guid",
        "itemName": "Laptop",
        "quantity": 2,
        "price": 999.99,
        "subtotal": 1999.98
      }
    ]
  }
]
```

### GET /api/orders/{id}
Get order by ID with all items

### GET /api/orders/number/{orderNumber}
Get order by order number (e.g., ORD202601280001)

### GET /api/orders/status/{status}
Get all orders by status (PENDING, PAID, or CANCELLED)

### POST /api/orders
Create a new order

**Request:**
```json
{
  "orderItems": [
    {
      "itemId": "guid-of-item",
      "quantity": 2
    },
    {
      "itemId": "another-guid",
      "quantity": 1
    }
  ]
}
```

**Features:**
- Auto-generates unique order number
- Validates all items exist
- Checks stock availability
- Reduces stock automatically
- Calculates total amount
- Sets status to PENDING
- Records order date and creation time

**Errors:**
- 400: Order must have at least one item
- 400: Item does not exist
- 400: Insufficient stock

### PATCH /api/orders/{id}/status
Update order status

**Request:**
```json
{
  "status": 1
}
```

**Status Values:**
- 0 = PENDING
- 1 = PAID
- 2 = CANCELLED

**Note:** Changing status to CANCELLED will restore stock

### POST /api/orders/{id}/cancel
Cancel an order and restore stock

**Response:**
```json
{
  "message": "Order cancelled successfully."
}
```

### DELETE /api/orders/{id}
Delete an order

## Complete Example Workflow

```bash
API_URL="http://localhost:5000"

# 1. Create Category
CATEGORY_ID=$(curl -s -X POST $API_URL/api/categories \
  -H "Content-Type: application/json" \
  -d '{"name": "Electronics", "description": "Electronic devices"}' | jq -r '.id')

# 2. Create Items
LAPTOP_ID=$(curl -s -X POST $API_URL/api/items \
  -H "Content-Type: application/json" \
  -d "{\"categoryId\": \"$CATEGORY_ID\", \"name\": \"Laptop\", \"price\": 999.99, \"stock\": 10}" | jq -r '.id')

PHONE_ID=$(curl -s -X POST $API_URL/api/items \
  -H "Content-Type: application/json" \
  -d "{\"categoryId\": \"$CATEGORY_ID\", \"name\": \"Smartphone\", \"price\": 699.99, \"stock\": 20}" | jq -r '.id')

# 3. Check initial stock
curl -s $API_URL/api/items | jq '.[] | {name, stock}'

# 4. Create Order (2 Laptops + 1 Phone)
ORDER_RESPONSE=$(curl -s -X POST $API_URL/api/orders \
  -H "Content-Type: application/json" \
  -d "{
    \"orderItems\": [
      {\"itemId\": \"$LAPTOP_ID\", \"quantity\": 2},
      {\"itemId\": \"$PHONE_ID\", \"quantity\": 1}
    ]
  }")

ORDER_ID=$(echo "$ORDER_RESPONSE" | jq -r '.id')
ORDER_NUMBER=$(echo "$ORDER_RESPONSE" | jq -r '.orderNumber')

echo "Order Created: $ORDER_NUMBER"
echo "$ORDER_RESPONSE" | jq

# 5. Check updated stock (Laptop: 10 -> 8, Phone: 20 -> 19)
curl -s $API_URL/api/items | jq '.[] | {name, stock}'

# 6. Get order by number
curl -s $API_URL/api/orders/number/$ORDER_NUMBER | jq

# 7. Get all pending orders
curl -s $API_URL/api/orders/status/PENDING | jq

# 8. Mark as PAID
curl -s -X PATCH $API_URL/api/orders/$ORDER_ID/status \
  -H "Content-Type: application/json" \
  -d '{"status": 1}' | jq

# 9. Get paid orders
curl -s $API_URL/api/orders/status/PAID | jq

# 10. Cancel order (stock will be restored)
curl -s -X POST $API_URL/api/orders/$ORDER_ID/cancel | jq

# 11. Check stock restored (Laptop: 8 -> 10, Phone: 19 -> 20)
curl -s $API_URL/api/items | jq '.[] | {name, stock}'

# 12. Get cancelled orders
curl -s $API_URL/api/orders/status/CANCELLED | jq
```

## Business Logic

### Creating an Order
1. Validates at least one item exists in the order
2. Checks each item exists in the database
3. Verifies sufficient stock for each item
4. Generates unique order number (ORD{YYYYMMDD}{0001})
5. Creates order with PENDING status
6. Creates order items with current item prices
7. Reduces stock for each item
8. Calculates and saves total amount

### Cancelling an Order
1. Finds the order
2. Checks if not already cancelled
3. Restores stock for all order items
4. Updates order status to CANCELLED

### Order Number Generation
- Format: `ORD{YYYYMMDD}{sequence}`
- Example: `ORD202601280001` (first order on Jan 28, 2026)
- Automatically increments sequence per day
- Next order same day: `ORD202601280002`

## Testing

Run the comprehensive test:
```bash
cd /Users/shofyan/dotnet/ecommerce
bash simple-order-test.sh
```

Or test manually with curl commands from the examples above.

## Error Handling

Common errors:
- **400 Bad Request**: Invalid data, insufficient stock, or item doesn't exist
- **404 Not Found**: Order not found
- **409 Conflict**: Order already cancelled or status conflict

All errors return JSON with descriptive messages:
```json
{
  "message": "Item with ID {id} does not exist."
}
```
