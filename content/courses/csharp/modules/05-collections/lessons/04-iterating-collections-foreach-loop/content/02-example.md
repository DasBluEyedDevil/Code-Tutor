---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;

// FOREACH WITH ARRAYS
string[] fruits = { "Apple", "Banana", "Orange", "Grape" };

Console.WriteLine("=== Fruits ===");
foreach (string fruit in fruits)
{
    Console.WriteLine($"I like {fruit}!");
}

// FOREACH WITH LISTS
List<int> scores = new List<int> { 95, 87, 92, 78, 88 };

Console.WriteLine("\n=== Scores ===");
foreach (int score in scores)
{
    Console.WriteLine($"Score: {score}");
}

// CALCULATING WITH FOREACH
int total = 0;
foreach (int score in scores)
{
    total += score;
}
double average = (double)total / scores.Count;
Console.WriteLine($"\nTotal: {total}, Average: {average:F2}");

// FINDING WITH FOREACH
int searchFor = 92;
bool found = false;

foreach (int score in scores)
{
    if (score == searchFor)
    {
        Console.WriteLine($"\nFound {searchFor}!");
        found = true;
        break;
    }
}

if (!found)
{
    Console.WriteLine($"\n{searchFor} not found");
}

// COUNTING WITH FOREACH
int passingCount = 0;
foreach (int score in scores)
{
    if (score >= 60)
    {
        passingCount++;
    }
}
Console.WriteLine($"\nPassing scores: {passingCount}");

// USEFUL COLLECTION METHODS
List<string> shoppingCart = new List<string>();

// .Add - add items
shoppingCart.Add("Milk");
shoppingCart.Add("Bread");
shoppingCart.Add("Eggs");
Console.WriteLine($"\nCart has {shoppingCart.Count} items");

// .Contains - check if item exists
if (shoppingCart.Contains("Milk"))
{
    Console.WriteLine("Milk is in the cart");
}

// .Remove - remove item
shoppingCart.Remove("Bread");
Console.WriteLine($"After removing bread: {shoppingCart.Count} items");

// .Clear - remove all
shoppingCart.Clear();
Console.WriteLine($"After clearing: {shoppingCart.Count} items");

// SORTING (Arrays)
int[] numbers = { 5, 2, 8, 1, 9 };
Array.Sort(numbers);  // Sorts in place: 1, 2, 5, 8, 9

Console.WriteLine("\n=== Sorted Numbers ===");
foreach (int num in numbers)
{
    Console.Write(num + " ");
}

// SORTING (Lists)
List<string> names = new List<string> { "Charlie", "Alice", "Bob" };
names.Sort();  // Alphabetical

Console.WriteLine("\n\n=== Sorted Names ===");
foreach (string name in names)
{
    Console.WriteLine(name);
}

// REVERSING
names.Reverse();  // Reverse order
Console.WriteLine("\n=== Reversed ===");
foreach (string name in names)
{
    Console.WriteLine(name);
}

// PRACTICAL: Finding min/max manually
int[] ages = { 25, 30, 18, 45, 22, 35 };
int oldest = ages[0];
int youngest = ages[0];

foreach (int age in ages)
{
    if (age > oldest)
        oldest = age;
    if (age < youngest)
        youngest = age;
}

Console.WriteLine($"\nOldest: {oldest}, Youngest: {youngest}");

// FOREACH WITH DICTIONARIES
Dictionary<string, int> inventory = new Dictionary<string, int>
{
    { "Apples", 50 },
    { "Oranges", 30 },
    { "Bananas", 25 }
};

Console.WriteLine("\n=== Inventory ===");
foreach (KeyValuePair<string, int> item in inventory)
{
    Console.WriteLine($"{item.Key}: {item.Value} units");
}

// Can also use var
foreach (var item in inventory)
{
    if (item.Value < 40)
    {
        Console.WriteLine($"Low stock: {item.Key}");
    }
}
```
