---
type: "WARNING"
title: "Important Considerations"
---

## new vs override - They're NOT the Same!

The `new` keyword HIDES a base method instead of overriding it. This breaks polymorphism!

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Woof!");  // Polymorphic!
}

class Cat : Animal
{
    public new void Speak() => Console.WriteLine("Meow!");  // HIDES - danger!
}

Animal dog = new Dog();
Animal cat = new Cat();

dog.Speak();  // "Woof!" - override works!
cat.Speak();  // "..." - new DOESN'T work polymorphically!

// Cat only meows when variable is Cat type:
Cat realCat = new Cat();
realCat.Speak();  // "Meow!"
```

**Key rules:**
- Always use `override` for polymorphic behavior
- `new` is rarely the right choice (it's usually a code smell)
- Compiler warning CS0114 means you need `override` or `new`

**Calling base implementation:**
```csharp
public override void Speak()
{
    base.Speak();  // Call base class version first
    Console.WriteLine("Woof!");  // Then add to it
}
```