---
type: "THEORY"
title: "ValueTask<T> - The Lightweight Alternative"
---

## When to Use ValueTask vs Task

**What is ValueTask?**
ValueTask<T> is a lightweight struct alternative to Task<T>. Unlike Task (which is a class and allocates on the heap), ValueTask can avoid allocations when the result is already available synchronously.

**When to use ValueTask:**
- Methods that OFTEN complete synchronously (e.g., cached values)
- High-performance hot paths where allocation matters
- Methods called very frequently in tight loops

**When to stick with Task:**
- Most normal async code (simpler and safer)
- When you need to await multiple times
- When you need Task.WhenAll/WhenAny (must convert with .AsTask())

**Important ValueTask restrictions:**
```csharp
ValueTask<int> GetValueAsync() { ... }

// DON'T await multiple times!
var vt = GetValueAsync();
var x = await vt;
var y = await vt;  // WRONG! Undefined behavior!

// DON'T block on it!
vt.Result;  // WRONG!
vt.GetAwaiter().GetResult();  // WRONG!

// If you need reuse, convert first:
Task<int> task = GetValueAsync().AsTask();
```

**Rule of thumb:** Start with Task<T>. Only use ValueTask<T> when profiling shows allocation is a bottleneck AND the method often completes synchronously.