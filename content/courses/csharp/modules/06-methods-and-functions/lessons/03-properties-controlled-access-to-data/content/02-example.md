---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class Person
{
    private int _age;  // Private field (backing field)

    // Property with validation
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0 && value <= 120)
                _age = value;
            else
                Console.WriteLine("Invalid age!");
        }
    }

    // Auto-implemented property (no backing field needed)
    public string Name { get; set; }

    // Init-only property (C# 9+) - can only be set during construction
    public string Id { get; init; }

    // Required property (C# 11+) - MUST be set when creating object
    public required string Email { get; set; }

    // Read-only property (get only)
    public string Status => Age >= 18 ? "Adult" : "Minor";

    // Expression-bodied property (C# 6+)
    public string Category => Age >= 65 ? "Senior" : "Regular";
}

// Usage
Person person = new Person
{
    Name = "Alice",
    Email = "alice@example.com",  // Required - must provide!
    Id = "P001"                    // Init-only - set once here
};
person.Age = 25;          // Works fine
person.Name = "Alicia";   // Can change later
// person.Id = "P002";    // ERROR! Init-only can't be changed after creation
Console.WriteLine($"{person.Name}: {person.Status}");
```
