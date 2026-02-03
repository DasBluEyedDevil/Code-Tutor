---
type: "KEY_POINT"
title: "Task<T>, WhenAll, and ValueTask"
---

## Key Takeaways

- **`Task<T>` represents a future result** -- `await` unwraps the `Task<string>` to get the `string`. Until awaited, the task may still be running.

- **Use `Task.WhenAll` for parallel I/O, `Task.WhenAny` for racing** -- `WhenAll` waits for all tasks to complete. `WhenAny` returns when the first task finishes, useful for timeout patterns.

- **Consider `ValueTask<T>` for hot-path methods** -- when a method often returns a cached result synchronously, `ValueTask<T>` avoids the heap allocation that `Task<T>` requires. Use it in high-performance library code.
