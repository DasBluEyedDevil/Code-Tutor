---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`async Task MethodName()`**: 'async' modifier enables await. 'Task' is return type (like void for async). Method body can contain 'await' expressions.

**`async Task<T> MethodName()`**: Async method that returns a value. Return type is Task<T> where T is the actual value type. Inside, you 'return T', not 'return Task<T>'!

**`await expression`**: Waits for async operation without blocking thread. Expression must be 'awaitable' (Task, Task<T>, or custom awaitable). Execution pauses, then resumes when complete.

**`Async all the way`**: If you call async method, you should await it. If your method awaits, it should be async. This propagates up the call stack - 'async all the way'!