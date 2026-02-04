---
type: "ANALOGY"
title: "💡 The Concept: Three Ways to Send Data"
---


### The Restaurant Order Analogy

Imagine ordering food at a restaurant:

**1. Path Parameters** = Table Number
- **Essential identifier** that's part of the resource location
- Usually required, not optional
- Identifies which specific resource you want

**2. Query Parameters** = Special Instructions
- **Optional filters or modifiers** that refine the request
- Can have multiple values
- Doesn't change which resource, but how you want it

**3. Request Body** = The Order Itself
- **Complex data** that doesn't fit in the URL
- Used for creating or updating resources
- Can contain nested structures

---



```kotlin
POST /orders
Body: {
  "items": ["burger", "fries"],
  "table": 12,
  "special_instructions": "no onions"
}
```
