---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System.Collections.Generic;

// Creating a list
List<string> shoppingList = new List<string>();

// Adding items
shoppingList.Add("Milk");
shoppingList.Add("Bread");
shoppingList.Add("Eggs");

Console.WriteLine("Items: " + shoppingList.Count);  // 3

// Accessing items (like arrays)
Console.WriteLine("First item: " + shoppingList[0]);

// Removing items
shoppingList.Remove("Bread");  // Removes "Bread"
Console.WriteLine("Items now: " + shoppingList.Count);  // 2

// Check if item exists
if (shoppingList.Contains("Milk"))
{
    Console.WriteLine("Don't forget milk!");
}

// Looping through a list
for (int i = 0; i < shoppingList.Count; i++)
{
    Console.WriteLine("Item " + i + ": " + shoppingList[i]);
}

// Create list with initial values
List<int> scores = new List<int> { 95, 87, 92, 78 };
```
