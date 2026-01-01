---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Traditional class - lots of boilerplate!
class PersonClass
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public PersonClass(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

// RECORD - one line does it all! (C# 9+)
public record Person(string Name, int Age);

// Usage
var person1 = new Person("Alice", 30);
var person2 = new Person("Alice", 30);

// Value-based equality - same data = equal!
Console.WriteLine(person1 == person2);  // True!

// Automatic ToString()
Console.WriteLine(person1);  // Person { Name = Alice, Age = 30 }

// With-expressions for copying with modifications
var olderPerson = person1 with { Age = 31 };
Console.WriteLine(olderPerson);  // Person { Name = Alice, Age = 31 }

// Original unchanged (immutable)
Console.WriteLine(person1);  // Person { Name = Alice, Age = 30 }

// RECORD STRUCT (C# 10+) - value type with record features
public record struct Point(int X, int Y);  // Mutable!
public readonly record struct ImmutablePoint(int X, int Y);  // Immutable!

var p1 = new Point(10, 20);
var p2 = p1 with { X = 30 };  // With-expression works!
Console.WriteLine(p2);  // Point { X = 30, Y = 20 }

// Nested records
public record Rectangle(Point TopLeft, Point BottomRight);
var rect = new Rectangle(new Point(0, 0), new Point(100, 50));
```
