---
type: "THEORY"
title: "Resource Naming Conventions (2025-2026 Standards)"
---

RULE 1: Use PLURAL NOUNS for collections:
✓ /api/users (not /api/user)
✓ /api/orders (not /api/order)
✓ /api/products (not /api/product)

RULE 2: Use KEBAB-CASE for multi-word names:
✓ /api/shipping-addresses
✓ /api/order-items
❌ /api/shippingAddresses (camelCase)
❌ /api/shipping_addresses (snake_case)

RULE 3: NO VERBS in URLs:
❌ /api/createUser
❌ /api/getUsers
❌ /api/deleteOrder

Why? The HTTP METHOD is the verb!
✓ POST /api/users (create)
✓ GET /api/users (retrieve)
✓ DELETE /api/orders/123 (delete)

RULE 4: Nesting to ONE level for relationships:
✓ /api/users/123/orders (user's orders)
✓ /api/orders/456/items (order's items)
❌ /api/users/123/orders/456/items/789/details (too deep!)