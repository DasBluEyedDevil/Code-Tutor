---
type: "KEY_POINT"
title: "Null Safety in Modern C#"
---

## Key Takeaways

- **`string?` means "this might be null"** -- the `?` suffix signals nullable intent. Without it, the compiler warns if you assign null. Always check nullable variables before using them.

- **Use `?.` and `??` for safe null handling** -- `name?.Length` returns null instead of crashing. `name ?? "default"` provides a fallback value. These two operators eliminate most null reference exceptions.

- **Avoid the `!` null-forgiving operator** -- `name!` tells the compiler "trust me, it is not null" but provides zero runtime protection. Prefer actual null checks or pattern matching: `if (name is string actualName)`.
