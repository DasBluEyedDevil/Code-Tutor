---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Voyager provides type-safe navigation for Compose Multiplatform** with features like back stack management, screen transitions, and lifecycle awareness. Each screen is a `Screen` class with a `@Composable Content()` method.

**Pass data between screens via screen constructor parameters**—this is type-safe and compile-time verified, unlike string-based route arguments. Write `navigator.push(DetailScreen(itemId = 42))`.

**Navigation belongs in the ViewModel or presentation logic**, not directly in composables. Composables should receive navigation callbacks, keeping UI code testable and decoupled from navigation implementation.
