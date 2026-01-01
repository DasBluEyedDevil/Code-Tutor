---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`CancellationToken`**: Pass to async methods to support cancellation. Check with 'token.ThrowIfCancellationRequested()'. Create with CancellationTokenSource. Essential for long operations!

**`ConfigureAwait(false)`**: In library code: 'await task.ConfigureAwait(false)' prevents deadlocks. In app code (UI/ASP.NET), usually not needed. Advanced topic!

**`IProgress<T>`**: Interface for reporting progress. Create with 'new Progress<T>(callback)'. Call 'progress.Report(value)' in async method. Useful for long operations with UI updates.

**`Async error handling`**: Use try/catch around await! Exceptions from awaited tasks propagate normally. Task.WhenAll aggregates exceptions - check task.Exception for all errors.