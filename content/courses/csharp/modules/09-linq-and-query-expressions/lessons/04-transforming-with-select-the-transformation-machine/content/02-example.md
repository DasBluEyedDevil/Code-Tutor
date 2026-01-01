---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Transform to same type
var squared = numbers.Select(n => n * n);
Console.WriteLine("Squared: " + string.Join(", ", squared));

var doubled = numbers.Select(n => n * 2);
Console.WriteLine("Doubled: " + string.Join(", ", doubled));

// Transform with objects
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
    new Person { Name = "Charlie", Age = 35, City = "Chicago" }
};

// Extract just names (object → string)
var names = people.Select(p => p.Name);
Console.WriteLine("Names: " + string.Join(", ", names));

// Extract ages (object → int)
var ages = people.Select(p => p.Age);
Console.WriteLine("Ages: " + string.Join(", ", ages));

// Transform to new anonymous object
var summaries = people.Select(p => new 
{ 
    Name = p.Name, 
    IsAdult = p.Age >= 18,
    Location = p.City
});

foreach (var summary in summaries)
{
    Console.WriteLine(summary.Name + " - Adult: " + summary.IsAdult + " - " + summary.Location);
}

// Combine .Where() and .Select()
var adultNames = people
    .Where(p => p.Age >= 30)
    .Select(p => p.Name);

Console.WriteLine("Adults (30+): " + string.Join(", ", adultNames));
```
