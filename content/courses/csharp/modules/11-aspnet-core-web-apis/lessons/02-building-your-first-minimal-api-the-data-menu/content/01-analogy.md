---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a food delivery app:
• BROWSE menu (GET /api/menu)
• VIEW item details (GET /api/menu/5)
• ADD to cart (POST /api/cart)
• UPDATE quantity (PUT /api/cart/3)
• REMOVE from cart (DELETE /api/cart/3)

Each of these is an API ENDPOINT! Together they form your API.

MINIMAL API (.NET 9 style):
• NO controllers needed!
• Define endpoints directly in Program.cs
• Lambda functions handle requests
• Less boilerplate, more productivity
• TypedResults for compile-time safety
• Built-in OpenAPI documentation

In-memory data store:
• For learning, use List<T> as database
• In production, you'd use real database (next module!)

Think: Minimal API = 'Quick, simple way to expose data and functionality over HTTP!'