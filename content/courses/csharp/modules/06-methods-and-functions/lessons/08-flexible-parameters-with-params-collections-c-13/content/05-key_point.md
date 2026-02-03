---
type: "KEY_POINT"
title: "params Collections in C# 13"
---

## Key Takeaways

- **C# 13 expands `params` beyond arrays** -- `params` now works with `IEnumerable<T>`, `ReadOnlySpan<T>`, `IReadOnlyList<T>`, and more. Callers can pass individual items, collection expressions, or existing collections.

- **`params ReadOnlySpan<T>` avoids heap allocations** -- for performance-critical code, span-based params keep data on the stack. This is a zero-cost abstraction for small argument lists.

- **`params` must be the last parameter** -- `void Log(string level, params string[] messages)` is valid. The compiler collects all remaining arguments into the params collection.
