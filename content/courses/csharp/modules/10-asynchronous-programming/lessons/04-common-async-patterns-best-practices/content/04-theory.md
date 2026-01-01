---
type: "THEORY"
title: "IAsyncEnumerable - Streaming Data Asynchronously"
---

## Async Streams with IAsyncEnumerable<T>

**The Problem:**
`Task<IEnumerable<T>>` waits for the ENTIRE collection before returning. What if you're loading thousands of records or streaming real-time data?

**The Solution: IAsyncEnumerable<T>**
Introduced in C# 8.0, it lets you yield results asynchronously as they become available!

```csharp
// BEFORE: Wait for ALL data
async Task<List<int>> GetAllDataAsync()
{
    var results = new List<int>();
    for (int i = 0; i < 100; i++)
    {
        await Task.Delay(100);
        results.Add(i);
    }
    return results;  // 10 seconds later, ALL at once!
}

// AFTER: Stream as you go!
async IAsyncEnumerable<int> StreamDataAsync()
{
    for (int i = 0; i < 100; i++)
    {
        await Task.Delay(100);
        yield return i;  // Available immediately!
    }
}

// Consuming async streams
await foreach (var item in StreamDataAsync())
{
    Console.WriteLine(item);  // Process each as it arrives!
}
```

**Best use cases:**
- Streaming database query results
- Real-time data feeds (stock prices, chat messages)
- Large file processing line by line
- API responses with pagination
- Any scenario where you want results ASAP without waiting for all