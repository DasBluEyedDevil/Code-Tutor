---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates LINQ in action, comparing the traditional loop approach with the concise LINQ query syntax.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;  // MUST include System.Linq!

List<int> numbers = new List<int> { 1, 5, 2, 8, 3, 9, 4, 7, 6 };

// WITHOUT LINQ - manual loop
List<int> evenNumbers = new List<int>();
foreach (int num in numbers)
{
    if (num % 2 == 0)
    {
        evenNumbers.Add(num);
    }
}
Console.WriteLine("Even (without LINQ): " + string.Join(", ", evenNumbers));

// WITH LINQ - one line!
var evenLinq = numbers.Where(n => n % 2 == 0);
Console.WriteLine("Even (with LINQ): " + string.Join(", ", evenLinq));

// More LINQ examples
var greaterThanFive = numbers.Where(n => n > 5);
var sorted = numbers.OrderBy(n => n);
var firstThree = numbers.Take(3);

Console.WriteLine("Greater than 5: " + string.Join(", ", greaterThanFive));
Console.WriteLine("Sorted: " + string.Join(", ", sorted));
Console.WriteLine("First 3: " + string.Join(", ", firstThree));

// QUERY SYNTAX (alternative)
var query = from n in numbers
            where n > 5
            orderby n descending
            select n;

Console.WriteLine("Query syntax: " + string.Join(", ", query));
```
