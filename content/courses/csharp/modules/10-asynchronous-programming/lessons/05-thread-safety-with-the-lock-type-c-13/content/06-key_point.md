---
type: "KEY_POINT"
title: "C# 13 Lock Type for Thread Safety"
---

## Key Takeaways

- **C# 13 introduces `System.Threading.Lock`** -- replace `private readonly object _lock = new();` with `private readonly Lock _lock = new();`. The compiler generates optimized code for the new type.

- **`lock (_lock) { }` protects shared state** -- only one thread can execute the code inside the lock block at a time. Use it when multiple threads read and write the same variable.

- **Keep lock sections small** -- lock only the minimum code that accesses shared state. Long-held locks cause thread contention and reduce performance. Never call external services inside a lock.
