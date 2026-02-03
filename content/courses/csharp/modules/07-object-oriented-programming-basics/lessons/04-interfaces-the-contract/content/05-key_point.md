---
type: "KEY_POINT"
title: "Interfaces Define Capabilities"
---

## Key Takeaways

- **Interfaces define WHAT, not HOW** -- `interface IDrawable { void Draw(); }` specifies the contract. The implementing class provides the actual code.

- **A class can implement multiple interfaces** -- `class Button : IDrawable, IClickable` is valid. This is how C# achieves the flexibility that multiple inheritance provides in other languages.

- **Program to interfaces, not implementations** -- use `IList<T>` as parameter types instead of `List<T>`. This makes your code flexible and testable because you can swap implementations.
