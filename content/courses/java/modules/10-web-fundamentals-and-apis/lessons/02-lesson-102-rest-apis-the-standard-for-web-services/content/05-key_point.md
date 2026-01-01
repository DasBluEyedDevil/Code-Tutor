---
type: "KEY_POINT"
title: "REST API is Like a Restaurant Menu"
---

MENU (API Documentation):
GET /api/dishes → View all dishes
POST /api/orders → Place an order
GET /api/orders/123 → Check order status

YOU (Client):
"I'll have the pasta" (POST /api/orders { "dish": "pasta" })

WAITER (HTTP):
Takes your order to kitchen

KITCHEN (Server):
Processes order, returns response

RESULT:
201 Created: { "order_id": 123, "dish": "pasta", "status": "preparing" }