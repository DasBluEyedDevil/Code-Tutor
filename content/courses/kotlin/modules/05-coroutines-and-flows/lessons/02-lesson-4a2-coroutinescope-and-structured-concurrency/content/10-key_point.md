---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Structured concurrency ensures coroutines are organized in a hierarchy** where parent scopes manage child coroutine lifecycles. When a scope is cancelled, all its children are cancelled automatically—no leaks, no orphaned work.

**`coroutineScope` suspends until all child coroutines complete**, while `supervisorScope` allows children to fail independently. Choose based on whether one failure should cancel all siblings (parallel tasks vs independent operations).

**Job cancellation is cooperative**—coroutines must check `isActive` or call suspending functions to respond to cancellation. CPU-intensive loops need explicit cancellation checks to avoid unresponsive coroutines.
