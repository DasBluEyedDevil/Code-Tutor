---
type: "WARNING"
title: "Important Considerations"
---

## Constructor Chaining with base()

When a base class has a constructor with parameters, derived classes MUST call it using `base()`:

```csharp
class Animal
{
    public string Name;
    public Animal(string name)  // Constructor with parameter
    {
        Name = name;
    }
}

class Dog : Animal
{
    public string Breed;
    
    // MUST call base constructor!
    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
    }
}

var dog = new Dog("Buddy", "Golden Retriever");
```

**Common pitfalls:**
- Forgetting to call `base()` when base class has no parameterless constructor
- Deep inheritance hierarchies (3+ levels) - harder to maintain!
- Using inheritance for code reuse when composition would be better

**The 'protected' access modifier:**
- `private`: Only the declaring class can access
- `protected`: Declaring class AND derived classes can access
- `public`: Anyone can access

Use `protected` for members that derived classes need but shouldn't be public!