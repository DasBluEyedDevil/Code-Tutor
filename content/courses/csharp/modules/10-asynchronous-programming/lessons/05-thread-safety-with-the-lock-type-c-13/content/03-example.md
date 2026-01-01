---
type: "EXAMPLE"
title: "Modern Thread Safety with Lock (C# 13)"
---

C# 13 introduces the dedicated Lock type for cleaner, more efficient synchronization.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

// ===== PROBLEM: Race condition without lock =====
class UnsafeCounter
{
    private int _count = 0;
    
    public void Increment()
    {
        // DANGER! Not thread-safe!
        // Multiple threads can read same value,
        // increment it, and write back - losing updates!
        _count++;
    }
    
    public int Count => _count;
}

// ===== OLD WAY: Using object as lock =====
class OldStyleCounter
{
    private int _count = 0;
    private readonly object _syncLock = new();  // Object as lock
    
    public void Increment()
    {
        lock (_syncLock)  // Only one thread at a time
        {
            _count++;  // Now thread-safe!
        }
    }
    
    public int Count
    {
        get
        {
            lock (_syncLock)
            {
                return _count;
            }
        }
    }
}

// ===== NEW WAY: C# 13 dedicated Lock type =====
class ModernCounter
{
    private int _count = 0;
    private readonly Lock _lock = new();  // Purpose-built Lock type!
    
    public void Increment()
    {
        lock (_lock)  // Compiler optimized!
        {
            _count++;
        }
    }
    
    // Alternative: EnterScope() pattern
    public void IncrementWithScope()
    {
        using (_lock.EnterScope())  // Auto-released when scope exits
        {
            _count++;
        }
    }
    
    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _count;
            }
        }
    }
}

// ===== DEMONSTRATION =====
var counter = new ModernCounter();
var tasks = new List<Task>();

Console.WriteLine("Starting 100 tasks, each incrementing 1000 times...");

for (int i = 0; i < 100; i++)
{
    tasks.Add(Task.Run(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            counter.Increment();
        }
    }));
}

await Task.WhenAll(tasks);

Console.WriteLine($"Final count: {counter.Count}");
Console.WriteLine($"Expected: 100000");
Console.WriteLine(counter.Count == 100000 ? "SUCCESS! Lock prevented race conditions!" : "ERROR: Race condition occurred!");
```
