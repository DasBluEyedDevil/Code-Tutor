---
type: "KEY_POINT"
title: "OOP Feature Decision Guide"
---

## Key Takeaways

- **Prefer composition over inheritance** -- "has-a" (Car HAS-A Engine) is usually more flexible than "is-a" (Car IS-A Vehicle). Composition avoids deep hierarchies and makes code easier to change.

- **Interface for capability, abstract class for shared code** -- if you need a contract with no shared logic, use an interface. If you want to share implementation between related classes, use an abstract class.

- **Keep inheritance hierarchies shallow** -- more than 3 levels deep becomes hard to understand and maintain. Flatten with interfaces and composition when the hierarchy grows.
