#!/bin/bash
API_URL="http://localhost:5000"

echo "=== Creating Category ==="
ELECTRONICS=$(curl -s -X POST $API_URL/api/categories -H "Content-Type: application/json" -d '{"name": "Electronics", "description": "Electronic devices"}' | jq -r '.id')
echo "Category ID: $ELECTRONICS"

echo -e "\n=== Creating Item ==="
LAPTOP=$(curl -s -X POST $API_URL/api/items -H "Content-Type: application/json" -d "{\"categoryId\": \"$ELECTRONICS\", \"name\": \"Laptop\", \"price\": 999.99, \"stock\": 10}" | jq -r '.id')
echo "Laptop ID: $LAPTOP"

echo -e "\n=== Creating Order ==="
curl -s -X POST $API_URL/api/orders -H "Content-Type: application/json" -d "{\"orderItems\": [{\"itemId\": \"$LAPTOP\", \"quantity\": 2}]}" | jq

echo -e "\n=== All Orders ==="
curl -s $API_URL/api/orders | jq
