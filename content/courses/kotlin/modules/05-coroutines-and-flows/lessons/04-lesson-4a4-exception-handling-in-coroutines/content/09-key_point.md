---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Exceptions in coroutines propagate up the job hierarchy**, cancelling parent and sibling coroutines by default. Use `supervisorScope` when you want to isolate failures and prevent cascading cancellation.

**CoroutineExceptionHandler catches unhandled exceptions** at the scope level, providing a last-resort error reporting mechanism. Install it on root scopes for logging and telemetry, but handle expected errors with try-catch in individual coroutines.

**`async` behaves differently from `launch` with exceptions**—errors are held in the Deferred result until you call `await()`. This allows explicit error handling at the point where you need the result.
