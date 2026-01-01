---
type: "WARNING"
title: "Task Pitfalls"
---

## Critical Task Mistakes!

**1. Task.WhenAll hides exceptions!**
When multiple tasks fail, WhenAll throws only the FIRST exception. Other exceptions are silently lost!
```csharp
try { await Task.WhenAll(tasks); }
catch { 
    // Check ALL tasks for errors!
    foreach (var t in tasks.Where(t => t.IsFaulted))
        Console.WriteLine(t.Exception);
}
```

**2. Sequential instead of parallel!**
```csharp
// SEQUENTIAL (slow!)
await Task1(); await Task2(); await Task3();

// PARALLEL (fast!)
var t1 = Task1(); var t2 = Task2(); var t3 = Task3();
await Task.WhenAll(t1, t2, t3);
```

**3. WhenAny returns the TASK, not the result!**
```csharp
Task<int> first = await Task.WhenAny(t1, t2);
int result = await first;  // Must await again!
```

**4. Using .Result blocks the thread!**
`task.Result` and `task.Wait()` can cause deadlocks. Always use `await`!