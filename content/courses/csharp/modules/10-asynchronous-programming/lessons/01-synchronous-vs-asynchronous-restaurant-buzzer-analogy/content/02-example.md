---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

// SYNCHRONOUS VERSION - blocks the thread
void DownloadFileSync()
{
    Console.WriteLine("  Starting sync download...");
    Thread.Sleep(3000);  // Blocks for 3 seconds!
    Console.WriteLine("  Sync download complete!");
}

// ASYNCHRONOUS VERSION - doesn't block
async Task DownloadFileAsync()
{
    Console.WriteLine("  Starting async download...");
    await Task.Delay(3000);  // Doesn't block!
    Console.WriteLine("  Async download complete!");
}

// DEMONSTRATION 1: Synchronous (blocking)
Console.WriteLine("=== SYNCHRONOUS (Blocking) ===");
Console.WriteLine("Before download");
DownloadFileSync();  // Program FREEZES here for 3 seconds!
Console.WriteLine("After download (had to wait)\n");

// Output:
// Before download
// Starting sync download...
// (3 second freeze - can't do anything!)
// Sync download complete!
// After download (had to wait)

// DEMONSTRATION 2: Asynchronous (non-blocking)
Console.WriteLine("=== ASYNCHRONOUS (Non-blocking) ===");
Console.WriteLine("Before download");
Task downloadTask = DownloadFileAsync();  // Starts but doesn't wait!
Console.WriteLine("Doing other work immediately!");
Console.WriteLine("Still working...");
await downloadTask;  // Now wait for it to finish
Console.WriteLine("After download\n");

// Output:
// Before download
// Starting async download...
// Doing other work immediately!  ← Runs DURING download!
// Still working...                ← Also DURING download!
// (after 3 seconds)
// Async download complete!
// After download

// REAL BENEFIT: Multiple async operations simultaneously
Console.WriteLine("=== MULTIPLE ASYNC OPERATIONS ===");
Task d1 = DownloadFileAsync();  // Start first
Task d2 = DownloadFileAsync();  // Start second
Task d3 = DownloadFileAsync();  // Start third

Console.WriteLine("All 3 downloads started!");
Console.WriteLine("Doing other work while all 3 run...");

await Task.WhenAll(d1, d2, d3);  // Wait for ALL to finish
Console.WriteLine("All downloads complete!");
// Total time: ~3 seconds (not 9!), because they ran SIMULTANEOUSLY!
```
