---
type: "KEY_POINT"
title: "Order Processing Pipeline"
---

## Key Takeaways

- **Orders follow a state machine** -- Created, Confirmed, Processing, Shipped, Delivered, Cancelled. Each transition has rules (cannot ship a cancelled order). Model these states explicitly in your domain.

- **Use transactions for order creation** -- creating an order, decrementing inventory, and clearing the cart must all succeed or all fail. Wrap in a database transaction to prevent partial operations.

- **Raise domain events for side effects** -- when an order is placed, raise `OrderPlacedEvent`. Handlers send confirmation emails, notify the warehouse, and update analytics without coupling the Order entity to those concerns.
