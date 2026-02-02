---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine delivering packages in a city:

Simple addresses (static routes):
- 123 Main Street (exact address)
- City Hall (exact location)
- Central Park (exact destination)

Dynamic addresses (parameterized routes):
- "Apartment {number} on Floor {floor}" -> Many apartments, one pattern
- "House number {X} on {Street Name}" -> Any house, flexible pattern
- "Locker {number} at Gym" -> Dynamic, but follows a pattern

Hono routing works the same way:
- Static routes: `/about`, `/contact` (exact paths)
- Dynamic routes: `/users/:id`, `/products/:category/:item` (pattern-based)

Parameters let you create one route that handles many similar requests!