---
type: "KEY_POINT"
title: "Abstract Classes as Contracts with Defaults"
---

## Key Takeaways

- **Abstract classes cannot be instantiated** -- `new Shape()` is a compiler error. You must create a derived class like `Circle` and instantiate that instead.

- **Abstract methods have no body and must be overridden** -- `abstract void Draw();` forces every derived class to provide its own implementation. Forgetting to override causes a compiler error.

- **Mix abstract and concrete members** -- abstract classes can contain fully implemented methods alongside abstract ones. This lets you share common logic while forcing subclass-specific behavior.
