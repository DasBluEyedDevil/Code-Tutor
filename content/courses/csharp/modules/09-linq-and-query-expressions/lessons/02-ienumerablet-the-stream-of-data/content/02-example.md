---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// IEnumerable<T> as return type
IEnumerable<int> GetNumbers()
{
    Console.WriteLine("Generating numbers...");
    yield return 1;
    yield return 2;
    yield return 3;
}

IEnumerable<int> numbers = GetNumbers();
Console.WriteLine("Method called, but not executed yet!");

foreach (int num in numbers)  // NOW it executes!
{
    Console.WriteLine("Number: " + num);
}

// LINQ returns IEnumerable<T>
List<int> sourceList = new List<int> { 1, 2, 3, 4, 5 };
IEnumerable<int> evenNumbers = sourceList.Where(n => n % 2 == 0);

Console.WriteLine("Query created, not executed!");

foreach (int num in evenNumbers)  // Executes here!
{
    Console.WriteLine("Even: " + num);
}

// Convert to concrete collection with .ToList() or .ToArray()
List<int> evenList = sourceList.Where(n => n % 2 == 0).ToList();
int[] evenArray = sourceList.Where(n => n % 2 == 0).ToArray();

Console.WriteLine("Count: " + evenList.Count);  // .Count on List (property)
Console.WriteLine("Count: " + evenNumbers.Count());  // .Count() on IEnumerable (method)
```
