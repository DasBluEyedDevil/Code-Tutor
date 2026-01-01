---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
class Animal
{
    public string Name;
    
    // VIRTUAL method - can be overridden
    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic animal sound");
    }
    
    public virtual void Move()
    {
        Console.WriteLine(Name + " is moving");
    }
}

class Dog : Animal
{
    // OVERRIDE the base method
    public override void MakeSound()
    {
        Console.WriteLine("Woof! Woof!");
    }
    
    public override void Move()
    {
        Console.WriteLine(Name + " is running on four legs");
    }
}

class Bird : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Tweet tweet!");
    }
    
    public override void Move()
    {
        Console.WriteLine(Name + " is flying");
    }
}

// Polymorphism in action
Animal animal1 = new Dog();
Animal animal2 = new Bird();

animal1.MakeSound();  // Calls Dog's version: Woof!
animal2.MakeSound();  // Calls Bird's version: Tweet!

// Even though both are declared as Animal, 
// they call their ACTUAL type's method!
```
