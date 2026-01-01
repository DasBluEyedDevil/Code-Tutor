---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System.Collections.Generic;

// Creating a dictionary
Dictionary<string, int> playerScores = new Dictionary<string, int>();

// Adding key-value pairs
playerScores.Add("Alice", 1500);
playerScores.Add("Bob", 1200);
playerScores.Add("Charlie", 1800);

// Or initialize with values
Dictionary<string, string> capitals = new Dictionary<string, string>
{
    { "USA", "Washington DC" },
    { "France", "Paris" },
    { "Japan", "Tokyo" }
};

// Accessing values by key
Console.WriteLine("Alice's score: " + playerScores["Alice"]);
Console.WriteLine("France's capital: " + capitals["France"]);

// Checking if key exists
if (playerScores.ContainsKey("Bob"))
{
    Console.WriteLine("Bob's score: " + playerScores["Bob"]);
}

// Updating a value
playerScores["Alice"] = 1600;  // Alice scored more!

// Looping through dictionary
foreach (var pair in playerScores)
{
    Console.WriteLine(pair.Key + " has " + pair.Value + " points");
}
```
