---
type: "WARNING"
title: "Critical Mistakes to Avoid"
---

## The Deadly Sins of Async Programming!

**1. async void is dangerous!**
Only use `async void` for event handlers! For everything else, use `async Task`. Why? `async void` methods can't be awaited, exceptions crash the process, and they're nearly impossible to test.

**2. The sync-over-async deadlock!**
```csharp
// DEADLOCK in UI/ASP.NET apps!
string result = GetDataAsync().Result;  // NEVER do this!
GetDataAsync().Wait();                   // Or this!
```
The calling thread blocks, but the async continuation needs that same thread. Result: deadlock! Always use `await`.

**3. Forgetting to await!**
```csharp
DoWorkAsync();  // Task starts but nobody waits!
// Exceptions are lost, completion is unknown!
```
Always await async methods unless you intentionally want fire-and-forget (and handle errors properly!).

**4. Not going 'async all the way'!**
If you start using async, propagate it UP the call stack. Mixing sync and async creates performance problems and deadlock risks.