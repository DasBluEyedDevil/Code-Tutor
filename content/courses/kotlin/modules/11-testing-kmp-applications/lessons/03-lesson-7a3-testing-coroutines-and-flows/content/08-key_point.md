---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Use `runTest` from kotlinx-coroutines-test** to test suspend functions and coroutines. It skips delays, advances virtual time, and ensures all coroutines complete before assertions.

**TestDispatchers provide deterministic coroutine execution** for unit tests. `StandardTestDispatcher` requires manual `advanceUntilIdle()`, giving you control over execution timing for complex scenarios.

**Test Flow emissions with `toList()` or `first()`**: `flow.toList()` collects all emissions into a list you can assert against. Use `turbine` library for more sophisticated Flow testing with timeouts and expectations.
