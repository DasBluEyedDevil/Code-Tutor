---
type: "WARNING"
title: "Common Pitfalls"
---

## Shopping Cart Pitfalls

**Race Conditions on Stock Checks**: Checking stock availability when adding to cart but not when placing the order creates a window where two users can both "reserve" the last item. Use database-level concurrency (optimistic concurrency with row versions, or SELECT FOR UPDATE) at checkout time to prevent overselling.

**Cart Data Loss on Session Expiry**: If the shopping cart lives only in memory or a session cookie, closing the browser loses the cart. For authenticated users, persist carts to the database. For anonymous users, consider local storage with a merge strategy when they eventually log in.

**Not Testing Before Deploying Cart Changes**: The shopping cart touches inventory, pricing, and user state simultaneously. A bug in cart logic (wrong quantity calculation, price not updating) is immediately visible to every user. Write integration tests that cover the full add-to-cart through checkout flow before deploying changes.
