---
type: "WARNING"
title: "Important Considerations"
---

## Abstract Classes Can Have Constructors!

Even though you can't instantiate an abstract class directly, it CAN have a constructor. Derived classes call it via `base()`:

```csharp
abstract class Animal
{
    public string Name { get; }
    
    // Constructor in abstract class!
    protected Animal(string name)
    {
        Name = name;
    }
    
    public abstract void Speak();
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }  // Must call base!
    public override void Speak() => Console.WriteLine($"{Name} says Woof!");
}

var dog = new Dog("Buddy");  // Works!
// var animal = new Animal("?");  // ERROR - can't instantiate abstract!
```

**Abstract vs Virtual:**
- `abstract`: NO implementation, derived classes MUST override
- `virtual`: HAS implementation, derived classes CAN override

**When to use abstract classes:**
- You have shared code (constructors, fields, methods)
- You want to force derived classes to implement specific methods
- There's a clear IS-A relationship in your hierarchy

**Abstract class gotchas:**
- Can only inherit from ONE abstract class (single inheritance)
- If a derived class is also abstract, it doesn't need to implement abstract methods
- Abstract properties and indexers are also supported!