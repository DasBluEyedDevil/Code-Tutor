---
type: "WARNING"
title: "Common Pitfalls"
---

## Async Gotchas to Avoid!

**1. Never use Thread.Sleep() in async code!**
Thread.Sleep() blocks the entire thread, defeating the purpose of async. Always use `await Task.Delay()` instead.

**2. Don't mix sync and async carelessly!**
Calling `.Result` or `.Wait()` on a Task in UI/ASP.NET apps can cause DEADLOCKS. The sync call blocks waiting for the async operation, but the async operation needs that same thread to continue!

**3. Async doesn't mean parallel!**
Async is about not BLOCKING, not about running things simultaneously. In a single-threaded context (like UI), tasks are interleaved, not truly parallel. Use Task.Run() for CPU-bound parallelism.

**4. Fire-and-forget is dangerous!**
Calling `DoWorkAsync()` without await starts the task but you lose track of it. Exceptions are silently swallowed! Always await or store the Task for later handling.