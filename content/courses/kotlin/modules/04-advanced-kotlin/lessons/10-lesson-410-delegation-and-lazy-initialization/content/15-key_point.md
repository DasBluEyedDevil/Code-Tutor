---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Delegation with `by` keyword eliminates wrapper boilerplate** by automatically forwarding interface methods to a delegate. This implements the delegation pattern with zero ceremony compared to explicit forwarding.

**`lazy` provides thread-safe, on-demand initialization** with caching. Use `val expensiveValue by lazy { computeValue() }` to defer expensive computations until first access, then reuse the result.

**Property delegation with custom delegates** enables cross-cutting concerns like observable properties, value validation, and persistent storage without cluttering business logic.
