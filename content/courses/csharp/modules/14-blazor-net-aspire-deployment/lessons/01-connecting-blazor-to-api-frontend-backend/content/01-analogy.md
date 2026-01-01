---
type: "ANALOGY"
title: "Understanding the Concept"
---

Full Stack = Frontend + Backend working together:

FRONTEND (Blazor):
• The restaurant customer
• Sees menu, orders food
• Nice UI, interactive

BACKEND (ASP.NET Core API):
• The kitchen
• Prepares the food
• Database, business logic

HttpClient = The waiter who connects them!

Blazer ← HttpClient → API ← EF Core → Database

Blazor calls API:
1. User clicks button
2. Blazor makes HTTP request
3. API processes (database query)
4. API sends JSON response
5. Blazor displays data

Think: HttpClient = 'The bridge between your beautiful UI and powerful backend!'