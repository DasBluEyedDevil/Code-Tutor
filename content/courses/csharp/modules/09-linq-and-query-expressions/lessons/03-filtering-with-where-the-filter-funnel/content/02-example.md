---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Simple filter
var evens = numbers.Where(n => n % 2 == 0);
Console.WriteLine("Evens: " + string.Join(", ", evens));

// Multiple conditions with AND
var range = numbers.Where(n => n > 3 && n < 8);
Console.WriteLine("Between 3 and 8: " + string.Join(", ", range));

// OR conditions
var extremes = numbers.Where(n => n <= 2 || n >= 9);
Console.WriteLine("Extremes: " + string.Join(", ", extremes));

// Working with objects
class Person
{
    public string Name;
    public int Age;
    public string City;
}

List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Age = 30, City = "NYC" },
    new Person { Name = "Bob", Age = 25, City = "LA" },
    new Person { Name = "Charlie", Age = 35, City = "NYC" },
    new Person { Name = "Diana", Age = 28, City = "Chicago" }
};

// Filter by property
var inNYC = people.Where(p => p.City == "NYC");
foreach (var person in inNYC)
{
    Console.WriteLine(person.Name + " lives in NYC");
}

// Complex filter: adults in NYC
var adultsInNYC = people.Where(p => p.Age >= 30 && p.City == "NYC");
foreach (var person in adultsInNYC)
{
    Console.WriteLine(person.Name + " is 30+ in NYC");
}

// Method calls in filter
var startsWithC = people.Where(p => p.Name.StartsWith("C"));
foreach (var person in startsWithC)
{
    Console.WriteLine("Starts with C: " + person.Name);
}
```
