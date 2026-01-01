---
type: "EXAMPLE"
title: "Code Example"
---

The `break` statement exits a loop immediately, while `continue` skips the rest of the current iteration and moves to the next one. Both give you fine-grained control over loop execution beyond the main condition.

```csharp
// BREAK example - exit loop early
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"Number: {i}");
    
    if (i == 5)
    {
        Console.WriteLine("Found 5! Exiting loop.");
        break;  // Exit the entire loop
    }
}
Console.WriteLine("Loop ended.");
// Only prints 1, 2, 3, 4, 5, then exits!

// CONTINUE example - skip current iteration
for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0)  // If even number
    {
        continue;  // Skip the rest, go to next iteration
    }
    
    Console.WriteLine($"Odd number: {i}");
}
// Only prints odd numbers: 1, 3, 5, 7, 9

// Real-world: Finding a player in a list
string[] players = { "Alice", "Bob", "Charlie", "David" };
string targetPlayer = "Charlie";

// Using foreach with break
foreach (string player in players)
{
    if (player == targetPlayer)
    {
        Console.WriteLine($"Found player: {player}");
        break;  // Found them, stop searching!
    }
}

// Filtering with continue
int[] scores = { 85, 42, 91, 55, 78, 33, 95 };
Console.WriteLine("High scores (75+):");

foreach (int score in scores)
{
    if (score < 75)
    {
        continue;  // Skip low scores
    }
    Console.WriteLine($"  {score}");
}

// Early exit from while loop
int attempts = 0;
while (true)  // Infinite loop - controlled by break
{
    attempts++;
    Console.WriteLine($"Attempt {attempts}");
    
    if (attempts >= 3)
    {
        Console.WriteLine("Max attempts reached!");
        break;  // Exit the infinite loop
    }
}
```
