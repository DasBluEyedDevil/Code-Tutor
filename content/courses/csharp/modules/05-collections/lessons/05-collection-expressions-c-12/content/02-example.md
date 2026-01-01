---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// OLD WAY - verbose and inconsistent
int[] oldArray = new int[] { 1, 2, 3, 4, 5 };
List<int> oldList = new List<int> { 1, 2, 3, 4, 5 };

// NEW WAY - Collection Expressions (C# 12)!
int[] numbers = [1, 2, 3, 4, 5];
List<int> scores = [95, 87, 92, 78, 88];
string[] names = ["Alice", "Bob", "Charlie"];

// Empty collections
int[] empty = [];
List<string> emptyList = [];

// SPREAD OPERATOR - combine collections!
int[] first = [1, 2, 3];
int[] second = [4, 5, 6];
int[] combined = [..first, ..second];  // [1, 2, 3, 4, 5, 6]

// Mix values and spreads
int[] withExtra = [0, ..first, 4, 5];  // [0, 1, 2, 3, 4, 5]

// Works with method calls too!
void PrintNumbers(int[] nums)
{
    foreach (var n in nums)
        Console.WriteLine(n);
}

PrintNumbers([10, 20, 30]);  // Pass inline!

// Great for building collections conditionally
int bonus = 100;
List<int> allScores = [..scores, bonus];  // Add bonus to end
Console.WriteLine(string.Join(", ", allScores));

// Works with Span<T> for high-performance code
Span<int> span = [1, 2, 3, 4, 5];
ReadOnlySpan<char> chars = ['H', 'e', 'l', 'l', 'o'];
```
