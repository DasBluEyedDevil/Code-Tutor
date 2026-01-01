---
type: "WARNING"
title: "Thread Safety Dangers"
---

## Critical Lock Mistakes!

**1. NEVER lock on 'this' or public objects!**
```csharp
lock (this) { }  // DANGEROUS! External code can deadlock you!
lock (typeof(MyClass)) { }  // DANGEROUS! Global lock!
```
Always use private readonly lock objects!

**2. NEVER lock on strings!**
```csharp
lock ("mylock") { }  // DANGEROUS! String interning means
                     // unrelated code might lock on same object!
```

**3. Don't hold locks for long operations!**
```csharp
lock (_lock)
{
    await Task.Delay(5000);  // WRONG! Never await inside lock!
    Thread.Sleep(5000);       // WRONG! Blocks all other threads!
}
```
Locks should protect SHORT, FAST operations only.

**4. Watch for deadlocks with multiple locks!**
```csharp
// Thread 1: lock(A) then lock(B)
// Thread 2: lock(B) then lock(A)
// DEADLOCK! Each waits for the other!
```
Always acquire locks in the SAME ORDER everywhere.

**5. The C# 13 Lock type requires .NET 9!**
`System.Threading.Lock` is only available in .NET 9+. For earlier versions, use the classic `object` lock pattern.

**6. Lock doesn't make everything thread-safe!**
Returning references to internal data can break thread safety even with locks. Return copies, not references!