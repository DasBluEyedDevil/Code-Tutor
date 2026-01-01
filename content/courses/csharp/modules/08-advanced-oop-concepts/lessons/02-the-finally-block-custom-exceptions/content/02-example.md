---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// FINALLY block example
try
{
    Console.WriteLine("Opening file...");
    // Risky file operations
    throw new Exception("File corrupted!");
    Console.WriteLine("This won't run");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
finally
{
    Console.WriteLine("Closing file...");  // ALWAYS runs!
    // Cleanup code here
}

// CUSTOM EXCEPTION - create your own!
class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message)
    {
    }
}

class Person
{
    private int _age;
    
    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0 || value > 120)
            {
                throw new InvalidAgeException("Age must be between 0 and 120!");
            }
            _age = value;
        }
    }
}

// Using custom exception
try
{
    Person person = new Person();
    person.Age = 150;  // Throws InvalidAgeException!
}
catch (InvalidAgeException ex)
{
    Console.WriteLine("Custom error: " + ex.Message);
}
finally
{
    Console.WriteLine("Validation complete.");
}
```
