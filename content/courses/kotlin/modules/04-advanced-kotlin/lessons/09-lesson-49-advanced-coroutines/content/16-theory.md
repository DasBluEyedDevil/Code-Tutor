---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered advanced coroutines. Here's what you learned:

✅ **Structured Concurrency** - `coroutineScope` and `supervisorScope`
✅ **Exception Handling** - Try-catch patterns and exception handlers
✅ **Flows** - Reactive streams with operators and transformations
✅ **Channels** - Communication between coroutines
✅ **StateFlow/SharedFlow** - State management and event broadcasting
✅ **Context Switching** - `withContext` for dispatcher changes

### Key Takeaways

1. **Use `coroutineScope`** for related tasks that should fail together
2. **Use `supervisorScope`** for independent tasks
3. **Flows are cold** (start on collection), **Channels are hot**
4. **StateFlow** for state, **SharedFlow** for events
5. **Exception handling** in `launch` requires `CoroutineExceptionHandler`
6. **`flowOn`** changes dispatcher for upstream operators

### Next Steps

In the next lesson, we'll explore **Delegation and Lazy Initialization** - powerful patterns for delegating behavior and optimizing resource usage!

---

**Practice Challenge**: Build a stock price monitoring system that fetches prices from multiple sources using Flows, combines them, and alerts when prices cross thresholds using StateFlow.

