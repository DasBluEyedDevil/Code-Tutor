---
type: "THEORY"
title: "📝 Lesson Checkpoint Quiz"
---


### Question 1
Which parameter type should you use for a required user ID in a route like "get user profile"?

A) Query parameter: `/profile?userId=42`
B) Path parameter: `/profile/42`
C) Request body: `POST /profile` with `{"userId": 42}`
D) Header: `X-User-ID: 42`

---

### Question 2
You're building a product search API with many optional filters (category, price range, brand, color). What's the BEST approach?

A) Use all path parameters: `/products/electronics/100/500/apple/red`
B) Use all query parameters: `/products?category=electronics&minPrice=100...`
C) Use request body for all filters: `POST /products/search`
D) Create separate endpoints for each filter combination

---

### Question 3
What does `call.request.queryParameters["page"]?.toIntOrNull() ?: 1` do?

A) Gets the page parameter and throws an error if it's not a number
B) Gets the page parameter, converts to Int, or returns 1 if null/invalid
C) Sets the page parameter to 1
D) Gets the first page of results

---

