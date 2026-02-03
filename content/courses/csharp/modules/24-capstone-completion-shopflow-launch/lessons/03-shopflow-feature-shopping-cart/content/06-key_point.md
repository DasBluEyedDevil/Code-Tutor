---
type: "KEY_POINT"
title: "Shopping Cart Implementation"
---

## Key Takeaways

- **Carts need both authenticated and guest support** -- store guest carts in a cookie or session. When the user logs in, merge the guest cart with their persisted cart. This prevents lost items during authentication.

- **Validate stock before checkout** -- check product availability when adding to cart AND at checkout time. Between those moments, other users may have purchased the last item.

- **Use Domain-Driven Design patterns** -- the Cart is an aggregate root. CartItems are value objects owned by the Cart. Apply business rules (max quantity, minimum order) in the domain layer, not the API layer.
