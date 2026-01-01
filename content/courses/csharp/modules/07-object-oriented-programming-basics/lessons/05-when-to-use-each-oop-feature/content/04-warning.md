---
type: "WARNING"
title: "Important Considerations"
---

## The 'sealed' Keyword - Preventing Inheritance

Sometimes you DON'T want a class to be inherited! Use `sealed` to prevent it:

```csharp
sealed class FinalImplementation
{
    // No class can inherit from this!
}

class Animal { public virtual void Speak() { } }
class Dog : Animal
{
    // sealed on a method prevents FURTHER overriding
    public sealed override void Speak() { Console.WriteLine("Woof"); }
}
class Poodle : Dog
{
    // ERROR! Can't override sealed method
    // public override void Speak() { } 
}
```

**When to use sealed:**
- Security-sensitive classes that shouldn't be extended
- Performance optimization (sealed classes can be slightly faster)
- When inheritance would break your class's invariants
- Framework/library classes where extension isn't intended

**Favor Composition Over Inheritance:**
Instead of: `class Car : Vehicle, Engine, Transmission` (impossible in C#!)
Do: `class Car { Vehicle type; Engine engine; Transmission trans; }` (composition)

Composition is more flexible - you can swap components at runtime!