---
type: "EXAMPLE"
title: "Code Example"
---

This example compares the modern way (Interpolation) with the old way (Concatenation).

```csharp
// --- The Modern Way: String Interpolation ---
// usage: Put a $ before the quotes, and use {} for variables.

string name = "Alex";
int age = 25;

Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"You are {age} years old.");

// You can even do math inside the curly braces!
Console.WriteLine($"In 10 years, you will be {age + 10}.");


// --- The Old Way: Concatenation (Avoid this!) ---
// usage: using + to glue text together.
// It's harder to read and easy to forget spaces.

Console.WriteLine("Hello, " + name + "!");
Console.WriteLine("You are " + age + " years old.");
```
