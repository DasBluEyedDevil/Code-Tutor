---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Flow is Kotlin's reactive stream API** for asynchronous sequences that emit multiple values over time. Unlike suspend functions that return a single value, Flows represent data streams like user events, sensor readings, or database changes.

**Flows are cold by default**—they don't start emitting until collected. This lazy evaluation means creating a Flow is cheap, and each collector gets its own independent execution of the Flow pipeline.

**Flow operators like `map`, `filter`, `transform`, and `buffer` are composable** and suspend-aware. Build complex data processing pipelines that respect cancellation and propagate exceptions through the stream.
