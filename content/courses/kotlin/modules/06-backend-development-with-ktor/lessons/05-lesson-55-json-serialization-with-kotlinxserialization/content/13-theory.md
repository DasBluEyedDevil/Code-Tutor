---
type: "THEORY"
title: "✅ Solution & Explanation"
---



### Testing


---



```bash
curl -X POST http://localhost:8080/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "T-Shirt",
    "description": "100% Cotton T-Shirt",
    "basePrice": 19.99,
    "category": "CLOTHING",
    "variants": [
      {
        "sku": "TS-RED-S",
        "attribute": "Red, Small",
        "stockQuantity": 50
      },
      {
        "sku": "TS-BLUE-L",
        "attribute": "Blue, Large",
        "stockQuantity": 30,
        "priceOverride": 24.99
      }
    ]
  }'
```
