# API cURL Request Examples

Base URL: `https://localhost:5001` or `http://localhost:5000`

## Categories API

### 1. Create Category
```bash
curl -X POST https://localhost:5001/api/categories \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Electronics",
    "description": "Electronic devices and accessories"
  }'
```

### 2. Create Another Category
```bash
curl -X POST https://localhost:5001/api/categories \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Clothing",
    "description": "Fashion and apparel items"
  }'
```

### 3. Get All Categories
```bash
curl -X GET https://localhost:5001/api/categories
```

### 4. Get Category by ID
```bash
# Replace {category-id} with actual GUID
curl -X GET https://localhost:5001/api/categories/{category-id}
```

### 5. Update Category
```bash
# Replace {category-id} with actual GUID
curl -X PUT https://localhost:5001/api/categories/{category-id} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Electronics & Gadgets",
    "description": "Updated description for electronic devices"
  }'
```

### 6. Delete Category
```bash
# Replace {category-id} with actual GUID
curl -X DELETE https://localhost:5001/api/categories/{category-id}
```

---

## Items API

### 1. Create Item
```bash
# Replace {category-id} with actual category GUID
curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "{category-id}",
    "name": "Laptop",
    "price": 999.99,
    "stock": 10
  }'
```

### 2. Create Multiple Items
```bash
# Item 1: Smartphone
curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "{category-id}",
    "name": "Smartphone",
    "price": 699.99,
    "stock": 25
  }'

# Item 2: Headphones
curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "{category-id}",
    "name": "Wireless Headphones",
    "price": 149.99,
    "stock": 50
  }'

# Item 3: Tablet
curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "{category-id}",
    "name": "Tablet",
    "price": 499.99,
    "stock": 15
  }'
```

### 3. Get All Items
```bash
curl -X GET https://localhost:5001/api/items
```

### 4. Get Item by ID
```bash
# Replace {item-id} with actual GUID
curl -X GET https://localhost:5001/api/items/{item-id}
```

### 5. Get Items by Category
```bash
# Replace {category-id} with actual category GUID
curl -X GET https://localhost:5001/api/items/category/{category-id}
```

### 6. Update Item
```bash
# Replace {item-id} with actual GUID
curl -X PUT https://localhost:5001/api/items/{item-id} \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "{category-id}",
    "name": "Gaming Laptop",
    "price": 1299.99,
    "stock": 8
  }'
```

### 7. Delete Item
```bash
# Replace {item-id} with actual GUID
curl -X DELETE https://localhost:5001/api/items/{item-id}
```

---

## Orders API

### 1. Create Order
```bash
# Replace {item-id} with actual item GUID
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{
    "orderItems": [
      {
        "itemId": "{item-id}",
        "quantity": 2
      },
      {
        "itemId": "{another-item-id}",
        "quantity": 1
      }
    ]
  }'
```

### 2. Get All Orders
```bash
curl -X GET http://localhost:5000/api/orders
```

### 3. Get Order by ID
```bash
# Replace {order-id} with actual GUID
curl -X GET http://localhost:5000/api/orders/{order-id}
```

### 4. Get Order by Order Number
```bash
# Replace {order-number} with actual order number (e.g., ORD202601280001)
curl -X GET http://localhost:5000/api/orders/number/{order-number}
```

### 5. Get Orders by Status
```bash
# Status can be: PENDING (0), PAID (1), CANCELLED (2)
curl -X GET http://localhost:5000/api/orders/status/PENDING
curl -X GET http://localhost:5000/api/orders/status/PAID
curl -X GET http://localhost:5000/api/orders/status/CANCELLED
```

### 6. Update Order Status
```bash
# Replace {order-id} with actual GUID
# Status: 0 = PENDING, 1 = PAID, 2 = CANCELLED
curl -X PATCH http://localhost:5000/api/orders/{order-id}/status \
  -H "Content-Type: application/json" \
  -d '{
    "status": 1
  }'
```

### 7. Cancel Order (Restores Stock)
```bash
# Replace {order-id} with actual GUID
curl -X POST http://localhost:5000/api/orders/{order-id}/cancel
```

### 8. Delete Order
```bash
# Replace {order-id} with actual GUID
curl -X DELETE http://localhost:5000/api/orders/{order-id}
```

---

## Complete Workflow Example

### Step 1: Create a Category and Save the ID
```bash
# Create Electronics category
curl -X POST https://localhost:5001/api/categories \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Electronics",
    "description": "Electronic devices and accessories"
  }' | jq

# Save the returned "id" value for next steps
```

### Step 2: Create Items in that Category
```bash
# Use the category ID from Step 1
CATEGORY_ID="paste-your-category-id-here"

curl -X POST https://localhost:5001/api/items \
  -H "Content-Type: application/json" \
  -d "{
    \"categoryId\": \"$CATEGORY_ID\",
    \"name\": \"Laptop\",
    \"price\": 999.99,
    \"stock\": 10
  }" | jq
```

### Step 3: Get All Items in Category
```bash
curl -X GET https://localhost:5001/api/items/category/$CATEGORY_ID | jq
```

### Step 4: Get All Categories
```bash
curl -X GET https://localhost:5001/api/categories | jq
```

### Step 5: Get All Items
```bash
curl -X GET https://localhost:5001/api/items | jq
```
### Step 6: Create an Order
```bash
# Use the item IDs from previous steps
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d "{
    \"orderItems\": [
      {\"itemId\": \"$ITEM_ID\", \"quantity\": 2}
    ]
  }" | jq
```

### Step 7: Get Order by Order Number
```bash
# Use the orderNumber from the order creation response
curl -X GET http://localhost:5000/api/orders/number/ORD202601280001 | jq
```

### Step 8: Check Updated Stock
```bash
# Stock should be reduced after order
curl -X GET http://localhost:5000/api/items | jq
```

### Step 9: Update Order Status to PAID
```bash
curl -X PATCH http://localhost:5000/api/orders/$ORDER_ID/status \
  -H "Content-Type: application/json" \
  -d '{"status": 1}' | jq
```

### Step 10: Cancel Order (Stock Restored)
```bash
curl -X POST http://localhost:5000/api/orders/$ORDER_ID/cancel | jq
```
---

## Notes

- Replace `{category-id}` and `{item-id}` with actual GUIDs returned from API responses
- Use `jq` for pretty JSON output (install with `brew install jq` on macOS)
- For Windows PowerShell, use `Invoke-RestMethod` or `Invoke-WebRequest` instead
- Add `-k` flag to curl if you have SSL certificate issues: `curl -k ...`
- All timestamps are in UTC format

## Testing with Variables

```bash
# Store base URL
API_URL="https://localhost:5001"

# Create category and extract ID (requires jq)
CATEGORY_RESPONSE=$(curl -s -X POST $API_URL/api/categories \
  -H "Content-Type: application/json" \
  -d '{"name": "Electronics", "description": "Electronic devices"}')

CATEGORY_ID=$(echo $CATEGORY_RESPONSE | jq -r '.id')
echo "Created Category ID: $CATEGORY_ID"

# Create item using the category ID
ITEM_RESPONSE=$(curl -s -X POST $API_URL/api/items \
  -H "Content-Type: application/json" \
  -d "{\"categoryId\": \"$CATEGORY_ID\", \"name\": \"Laptop\", \"price\": 999.99, \"stock\": 10}")

ITEM_ID=$(echo $ITEM_RESPONSE | jq -r '.id')
echo "Created Item ID: $ITEM_ID"

# Get the item
curl -X GET $API_URL/api/items/$ITEM_ID | jq

# Get items by category
curl -X GET $API_URL/api/items/category/$CATEGORY_ID | jq

# Update item
curl -X PUT $API_URL/api/items/$ITEM_ID \
  -H "Content-Type: application/json" \
  -d "{\"categoryId\": \"$CATEGORY_ID\", \"name\": \"Gaming Laptop\", \"price\": 1299.99, \"stock\": 5}" | jq

# Delete item
curl -X DELETE $API_URL/api/items/$ITEM_ID

# Delete category
curl -X DELETE $API_URL/api/categories/$CATEGORY_ID
```

## Error Responses

### 404 Not Found
```json
{
  "message": "Category with ID {id} not found."
}
```

### 400 Bad Request
```json
{
  "message": "Category with ID {id} does not exist."
}
```

### Validation Error
```json
{
  "errors": {
    "Name": ["The Name field is required."]
  }
}
```
