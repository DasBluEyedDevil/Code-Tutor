---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Protocol = Structural subtyping** - "Duck typing" with type hints
- **ABC = Nominal subtyping** - Explicit inheritance required
- **Use Protocol for** - Pure interfaces, third-party code, flexibility
- **Use ABC for** - Shared implementation, template method pattern
- **@runtime_checkable** - Enables isinstance() for Protocols
- **Dependency Injection** - Accept Protocol/ABC types, not concrete classes
- **Testing benefit** - Easy to create mock implementations
- **Type safety** - Caught by mypy/pyright at development time