---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Task<T> vs Task`**: Task<T> returns a value of type T. Task returns nothing (like void). Both represent async operations. Use await to get T from Task<T>.

**`await Task.WhenAll(t1, t2, t3)`**: Waits for ALL tasks to complete. Returns array of results if tasks are Task<T>. More efficient than awaiting each task sequentially!

**`await Task.WhenAny(t1, t2)`**: Waits for FIRST task to complete. Returns the completed task (not the result!). Await the returned task to get result. Useful for timeouts.

**`Task.Run(() => code)`**: Runs code on background thread (thread pool). Use for CPU-intensive work. Returns Task or Task<T>. Don't use for I/O (use async I/O instead).