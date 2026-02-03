---
type: "KEY_POINT"
title: "Virtual and Override for Polymorphism"
---

## Key Takeaways

- **`virtual` in the base class enables overriding** -- without `virtual`, derived classes cannot replace the method implementation. Mark methods `virtual` when you expect subclasses to customize behavior.

- **`override` in the derived class replaces the behavior** -- the signature must match exactly. When you call the method on a base-type variable holding a derived object, the overridden version runs.

- **`base.Method()` calls the parent implementation** -- use it inside an override when you want to extend rather than replace. Call `base` first for setup, or last for cleanup.
