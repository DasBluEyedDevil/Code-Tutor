---
type: "KEY_POINT"
title: "HTTP Methods are Like Restaurant Actions"
---

Imagine managing a restaurant menu database:

📖 GET /api/menu-items
Action: Browse the menu
Like: Customer looking at menu
Changes nothing, just reads

➕ POST /api/menu-items
Action: Add new dish to menu
Like: Chef creates new recipe
Server generates ID, returns location

🔄 PUT /api/menu-items/123
Action: Replace entire menu item
Like: Completely redesign a dish
Must send ALL fields (name, price, ingredients)

✏️ PATCH /api/menu-items/123
Action: Update only price
Like: Just change the price tag
Send only changed fields: {"price": 15.99}

🗑️ DELETE /api/menu-items/123
Action: Remove from menu
Like: Discontinue a dish
Item gone, can't be retrieved