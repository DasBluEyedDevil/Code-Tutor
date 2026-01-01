---
type: "EXAMPLE"
title: "Code Example"
---

C# supports all the math operators you'd expect, plus the modulus operator (%) which gives you the remainder after division. Pay close attention to integer division - it's a common source of bugs! The order of operations follows standard math rules (PEMDAS), but you can use parentheses to make your intent clear.

```csharp
// Basic math operations
int a = 10;
int b = 3;

int sum = a + b;        // 13
int difference = a - b; // 7
int product = a * b;    // 30
int quotient = a / b;   // 3 (integer division - decimal is dropped!)
int remainder = a % b;  // 1 (10 / 3 = 3 remainder 1)

Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Remainder: {remainder}");

// Order of operations matters!
int result1 = 5 + 3 * 2;     // 11 (multiplication first)
int result2 = (5 + 3) * 2;   // 16 (parentheses first)
Console.WriteLine($"{result1} vs {result2}");

// To get decimal results, use double or decimal
double preciseQuotient = 10.0 / 3.0;  // 3.333...
Console.WriteLine($"Precise: {preciseQuotient:F2}");  // Shows 3.33
```
