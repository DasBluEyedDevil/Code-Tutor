---
type: "KEY_POINT"
title: "Key Takeaways"
---

**StateFlow is a hot Flow that holds and emits the current state** to all collectors. It's Kotlin's reactive state holder, perfect for UI state management where new subscribers need the latest value immediately.

**SharedFlow is a hot Flow for events without state**. Unlike StateFlow, it doesn't cache the last value—use it for one-time events like navigation commands or toasts that shouldn't replay to new subscribers.

**Both StateFlow and SharedFlow are thread-safe** and can be safely updated from any coroutine. They handle concurrent collectors efficiently, making them ideal for multiplatform shared business logic layers.
