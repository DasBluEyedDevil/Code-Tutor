---
type: "ANALOGY"
title: "Real-World Analogy: The Restaurant Kitchen"
---

Think of your Serverpod server like a professional restaurant kitchen.

**The Endpoint Class = A Kitchen Station**

Like how a kitchen has stations (grill station, salad station, dessert station), your server has endpoint classes:
- UserEndpoint (handles user operations)
- PostEndpoint (handles post operations)
- OrderEndpoint (handles order operations)

**The Methods = Dishes You Can Order**

Each station can prepare specific dishes. Each endpoint has specific methods:
- UserEndpoint.getUser() - Get me a user (like ordering a steak)
- UserEndpoint.createUser() - Create a new user (like ordering a custom dish)
- PostEndpoint.listPosts() - Get all posts (like ordering the tasting menu)

**The Session = The Order Ticket**

When a waiter takes your order, they write a ticket with:
- Your table number (user authentication)
- Special requests (request context)
- The kitchen's resources (database access)

The Session parameter is like that ticket - it carries all the context needed to fulfill the request.

**The Flutter Client = The Waiter**

The generated Flutter client is like a waiter who:
- Knows all the dishes (methods) available
- Takes your order to the right station (endpoint)
- Brings back exactly what you ordered (typed response)
- Handles any problems (error handling)

