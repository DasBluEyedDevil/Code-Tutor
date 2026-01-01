---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

// Method returning Task<int> (promise of future int)
async Task<int> CalculateAsync(int x, int y)
{
    await Task.Delay(1000);  // Simulate work
    return x + y;
}

// Using Task<T>
Task<int> resultTask = CalculateAsync(5, 3);
Console.WriteLine("Calculation started...");

int result = await resultTask;  // Wait and get the int
Console.WriteLine("Result: " + result);

// TASK METHODS

// Task.WhenAll - wait for ALL tasks
Task<int> t1 = CalculateAsync(1, 2);
Task<int> t2 = CalculateAsync(3, 4);
Task<int> t3 = CalculateAsync(5, 6);

int[] results = await Task.WhenAll(t1, t2, t3);
Console.WriteLine("All results: " + string.Join(", ", results));

// Task.WhenAny - wait for FIRST to complete
Task<int> fast = Task.Delay(500).ContinueWith(_ => 1);
Task<int> slow = Task.Delay(2000).ContinueWith(_ => 2);

Task<int> firstDone = await Task.WhenAny(fast, slow);
int firstResult = await firstDone;
Console.WriteLine("First completed: " + firstResult);

// Task.Run - run CPU-bound work on background thread
Task<int> cpuTask = Task.Run(() => 
{
    int sum = 0;
    for (int i = 0; i < 1000000; i++)
    {
        sum += i;
    }
    return sum;
});

int sum = await cpuTask;
Console.WriteLine("Sum: " + sum);

// Task status properties
Task<string> task = GetDataAsync();

Console.WriteLine("IsCompleted: " + task.IsCompleted);
Console.WriteLine("Status: " + task.Status);

string data = await task;
Console.WriteLine("IsCompleted: " + task.IsCompleted);  // Now true!
```
