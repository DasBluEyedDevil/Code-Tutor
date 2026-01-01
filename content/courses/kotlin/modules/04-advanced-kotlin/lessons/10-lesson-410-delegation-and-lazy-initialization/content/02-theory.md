---
type: "THEORY"
title: "Topic Introduction"
---


In software development, you often want to reuse behavior from other classes or defer expensive operations until they're needed. Kotlin provides powerful delegation mechanisms that make these patterns simple and type-safe.

Delegation is the design pattern where an object handles a request by delegating to a helper object (delegate). Instead of inheritance, you compose objects and delegate behavior. Kotlin provides first-class language support for this pattern.

In this lesson, you'll learn:
- Class delegation with the `by` keyword
- Property delegation patterns
- Lazy initialization with `lazy`
- Observable properties
- Custom delegates
- Standard delegates: `notNull`, `vetoable`, `observable`

By the end, you'll write cleaner, more maintainable code using delegation!

---

