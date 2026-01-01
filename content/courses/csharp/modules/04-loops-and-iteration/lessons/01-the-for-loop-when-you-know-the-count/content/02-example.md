---
type: "EXAMPLE"
title: "Code Example"
---

The for loop has three parts in the parentheses: initialization (where to start), condition (when to stop), and increment (how to change the counter). Each part is separated by semicolons. The loop body executes repeatedly until the condition becomes false.

```csharp
// Basic for loop - count from 1 to 5
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine("Number: " + i);
}

// Countdown
for (int countdown = 10; countdown >= 1; countdown--)
{
    Console.WriteLine(countdown);
}
Console.WriteLine("Blast off!");

// Count by 2s
for (int i = 0; i <= 10; i += 2)
{
    Console.WriteLine("Even number: " + i);
}

// Real-world: print a multiplication table
int number = 5;
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{number} x {i} = {number * i}");
}

// Modern C#: Use foreach when iterating collections
string[] fruits = { "apple", "banana", "cherry" };
foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}

// Performance tip: for loops with arrays can be faster
// than foreach in performance-critical code
for (int i = 0; i < fruits.Length; i++)
{
    Console.WriteLine(fruits[i]);
}
```
