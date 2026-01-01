---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

// ASYNC method - returns Task
async Task DoWorkAsync()
{
    Console.WriteLine("Work started");
    await Task.Delay(1000);  // Pause for 1 second
    Console.WriteLine("Work completed");
}

// ASYNC method with return value - returns Task<string>
async Task<string> GetDataAsync()
{
    Console.WriteLine("Fetching data...");
    await Task.Delay(2000);
    return "Data retrieved!";  // Returns string, but method returns Task<string>
}

// ASYNC method calling other async methods
async Task ProcessDataAsync()
{
    Console.WriteLine("Starting process...");
    
    // Await other async methods
    await DoWorkAsync();
    
    string data = await GetDataAsync();  // Get returned value
    Console.WriteLine("Got: " + data);
    
    Console.WriteLine("Process complete!");
}

// Real-world example: HTTP request
async Task<string> DownloadWebPageAsync(string url)
{
    using HttpClient client = new HttpClient();  // Modern using declaration
    Console.WriteLine("Downloading " + url + "...");
    string content = await client.GetStringAsync(url);  // Async HTTP call
    Console.WriteLine("Download complete!");
    return content;  // client disposed at end of method scope
}

// Using the async methods
await ProcessDataAsync();

// Multiple sequential awaits
Console.WriteLine("Step 1");
await Task.Delay(500);
Console.WriteLine("Step 2");
await Task.Delay(500);
Console.WriteLine("Step 3");

// Calling async method without await (fire-and-forget)
Task backgroundTask = DoWorkAsync();  // Starts, doesn't wait
Console.WriteLine("This runs immediately!");
await backgroundTask;  // Now wait for it
```
