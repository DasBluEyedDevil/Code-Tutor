---
type: "THEORY"
title: "The New System.Threading.Lock Type"
---

## C# 13 Lock Type - What's New?

**Why a dedicated Lock type?**
1. **Clearer intent**: `Lock` explicitly says 'this is for synchronization'
2. **Compiler optimizations**: The compiler can generate more efficient code
3. **Better API**: Purpose-built methods like `EnterScope()`
4. **Type safety**: Can't accidentally use wrong object as lock

**The old approach (still works, but outdated):**
```csharp
private readonly object _syncLock = new();
lock (_syncLock) { /* critical section */ }
```

**The new C# 13 approach:**
```csharp
private readonly Lock _lock = new();
lock (_lock) { /* critical section - optimized! */ }
```

**EnterScope() pattern:**
```csharp
using (_lock.EnterScope())
{
    // Critical section - automatically released!
}
```

**When to use locks:**
- Accessing shared mutable state from multiple threads
- Incrementing counters, modifying collections
- Any read-modify-write operation

**Best practices:**
- Keep critical sections SHORT
- Always use `readonly` for lock objects
- Don't lock on `this` or public objects
- Prefer `Lock` over `object` in new C# 13+ code