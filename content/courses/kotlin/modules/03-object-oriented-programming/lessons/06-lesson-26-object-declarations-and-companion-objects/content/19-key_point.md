---
type: "KEY_POINT"
title: "Key Takeaways"
---

**`object` declarations create singletons—classes with exactly one instance**. Use them for stateless utilities, global configuration, or when you need exactly one coordinator for a subsystem.

**Companion objects provide class-level members** similar to Java's static methods, but with the power of inheritance and interfaces. They can implement interfaces and extend classes, making them more flexible than static members.

**Factory methods in companion objects are idiomatic Kotlin**. Create instances through named factory methods like `User.create(email)` instead of direct constructor calls when you need validation, caching, or multiple creation strategies.
