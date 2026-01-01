---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Comparison operators
int score = 85;

if (score == 100) { Console.WriteLine("Perfect!"); }
if (score != 100) { Console.WriteLine("Not perfect, but good!"); }
if (score > 80) { Console.WriteLine("Above 80!"); }
if (score >= 85) { Console.WriteLine("85 or higher!"); }

// Logical operators - AND (&&)
int age = 25;
bool hasLicense = true;

if (age >= 16 && hasLicense)
{
    Console.WriteLine("You can drive!");
}

// Logical operators - OR (||)
bool isWeekend = true;
bool isHoliday = false;

if (isWeekend || isHoliday)
{
    Console.WriteLine("No work today!");
}

// NOT operator (!)
bool isRaining = false;

if (!isRaining)
{
    Console.WriteLine("Let's go outside!");
}
```
