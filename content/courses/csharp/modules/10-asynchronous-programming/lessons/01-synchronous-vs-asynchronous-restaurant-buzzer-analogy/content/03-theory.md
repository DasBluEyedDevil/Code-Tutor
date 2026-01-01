---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Thread.Sleep() vs Task.Delay()`**: Thread.Sleep() BLOCKS the thread (synchronous). Task.Delay() is ASYNC - releases thread while waiting. Always prefer Task.Delay() in async code!

**`async Task MethodName()`**: 'async' keyword marks method as asynchronous. Return type is usually 'Task' (void equivalent) or 'Task<T>' (returns T). Enables 'await' inside.

**`await expression`**: 'await' says 'pause here until this Task completes, but release the thread for other work.' Can only use inside 'async' method.

**`Task.WhenAll()`**: Runs multiple async operations simultaneously! Waits for ALL to complete. Much faster than running one-by-one.