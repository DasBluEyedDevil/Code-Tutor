---
type: "EXAMPLE"
title: "Code Example"
---

This example shows string concatenation with the + operator, and introduces the modern string interpolation approach preferred in .NET 9.

```csharp
// METHOD 1: String Concatenation (using +)
Console.WriteLine("Hello" + " " + "World");
Console.WriteLine("I have " + 5 + " apples");
Console.WriteLine("The answer is: " + (2 + 2));

// METHOD 2: String Interpolation (MODERN - preferred!)
// Put $ before the string, then use {variable} inside
string name = "Alex";
int age = 25;

Console.WriteLine($"Hello, {name}!");           // Hello, Alex!
Console.WriteLine($"You are {age} years old."); // You are 25 years old.
Console.WriteLine($"Next year: {age + 1}");     // Next year: 26

// Interpolation is cleaner and easier to read!
// Compare:
Console.WriteLine("Name: " + name + ", Age: " + age);  // Concatenation
Console.WriteLine($"Name: {name}, Age: {age}");        // Interpolation
```
