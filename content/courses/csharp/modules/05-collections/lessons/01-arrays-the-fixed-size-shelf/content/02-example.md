---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Creating arrays
int[] scores = new int[5];  // Creates array with 5 empty slots
scores[0] = 95;  // First item (index 0)
scores[1] = 87;
scores[2] = 92;
scores[3] = 78;
scores[4] = 88;  // Last item (index 4, not 5!)

// Or initialize with values directly
string[] players = { "Alice", "Bob", "Charlie" };

// Accessing array items
Console.WriteLine("First player: " + players[0]);  // Alice
Console.WriteLine("Third player: " + players[2]);  // Charlie

// Array length
Console.WriteLine("Number of players: " + players.Length);

// Looping through an array
for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine("Score " + i + ": " + scores[i]);
}
```
