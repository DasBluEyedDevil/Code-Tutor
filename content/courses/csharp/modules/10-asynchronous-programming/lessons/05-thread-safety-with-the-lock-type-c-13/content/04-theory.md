---
type: "THEORY"
title: "Lock vs Other Synchronization Options"
---

## When to Use What?

**Lock (C# 13) / lock statement:**
- Simple mutual exclusion
- Short critical sections
- Most common choice for thread safety

**SemaphoreSlim:**
- Limit concurrent access (e.g., max 5 threads)
- Async-friendly with WaitAsync()

**ReaderWriterLockSlim:**
- Many readers, few writers scenario
- Readers don't block each other

**Interlocked:**
- Simple atomic operations (increment, compare-exchange)
- No lock overhead for single operations
- Example: `Interlocked.Increment(ref _count);`

**Monitor:**
- More control than lock (TryEnter, Wait, Pulse)
- lock statement is syntactic sugar for Monitor

**Concurrent collections:**
- ConcurrentDictionary, ConcurrentQueue, etc.
- Built-in thread safety for collections

**Rule of thumb:**
- Start with `Lock` (C# 13) or `lock` for simple cases
- Use `Interlocked` for single atomic operations
- Use concurrent collections for thread-safe data structures
- Consider other primitives for complex scenarios