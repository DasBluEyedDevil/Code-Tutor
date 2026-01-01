---
type: "EXAMPLE"
title: "Code Example"
---

Nested loops place one loop inside another. For each iteration of the outer loop, the inner loop runs completely. This creates a multiplicative effect: 3 outer iterations times 4 inner iterations equals 12 total inner loop executions. This pattern is perfect for grids, tables, patterns, and comparing pairs of items.

```csharp
// BASIC NESTED LOOP - GRID
for (int row = 0; row < 3; row++)
{
    for (int col = 0; col < 4; col++)
    {
        Console.Write("* ");
    }
    Console.WriteLine();  // New line after each row
}
// Output:
// * * * *
// * * * *
// * * * *

// MULTIPLICATION TABLE
for (int i = 1; i <= 5; i++)
{
    for (int j = 1; j <= 5; j++)
    {
        int result = i * j;
        Console.Write($"{result,4}");  // 4 spaces wide
    }
    Console.WriteLine();
}

// TRIANGLE PATTERN
for (int row = 1; row <= 5; row++)
{
    for (int col = 1; col <= row; col++)
    {
        Console.Write("*");
    }
    Console.WriteLine();
}
// Output: *, **, ***, ****, *****

// COMPARING ALL PAIRS - using foreach
string[] team1 = { "Alice", "Bob", "Charlie" };
string[] team2 = { "Diana", "Eve" };

Console.WriteLine("All possible matchups:");
foreach (string player1 in team1)
{
    foreach (string player2 in team2)
    {
        Console.WriteLine($"{player1} vs {player2}");
    }
}

// FINDING DUPLICATES
int[] numbers = { 1, 2, 3, 2, 4, 1, 5 };

for (int i = 0; i < numbers.Length; i++)
{
    for (int j = i + 1; j < numbers.Length; j++)
    {
        if (numbers[i] == numbers[j])
        {
            Console.WriteLine($"Duplicate found: {numbers[i]}");
        }
    }
}

// BREAKING OUT OF NESTED LOOPS
int[,] grid = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
int searchFor = 5;
bool found = false;

for (int r = 0; r < 3 && !found; r++)
{
    for (int c = 0; c < 3; c++)
    {
        if (grid[r, c] == searchFor)
        {
            Console.WriteLine($"Found {searchFor} at [{r},{c}]");
            found = true;
            break;  // Breaks inner loop, flag stops outer
        }
    }
}
```
