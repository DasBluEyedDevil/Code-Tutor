---
type: "KEY_POINT"
title: "Cancellation, Progress, and Async Streams"
---

## Key Takeaways

- **Always accept `CancellationToken` in long-running async methods** -- create with `CancellationTokenSource`, pass to methods, check with `token.ThrowIfCancellationRequested()`. This lets callers cancel operations gracefully.

- **`IAsyncEnumerable<T>` streams results as they arrive** -- instead of waiting for the entire collection, `await foreach` processes items one by one. Ideal for database cursors, file processing, and real-time data feeds.

- **Use try/catch around `await`** -- exceptions from awaited tasks propagate normally. `Task.WhenAll` aggregates multiple exceptions into an `AggregateException` accessible via `task.Exception`.
