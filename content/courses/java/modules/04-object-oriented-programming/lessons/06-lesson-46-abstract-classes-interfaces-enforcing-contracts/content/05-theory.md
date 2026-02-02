---
type: "THEORY"
title: "Abstract Class vs Interface - When to Use What?"
---

ABSTRACT CLASS:
✓ IS-A relationship (Dog IS-A Animal)
✓ Share CODE between subclasses (common fields, methods)
✓ Has state (fields)
✓ Single inheritance only

Example:
abstract class Animal {  // Common state and behavior
    String name;
    abstract void makeSound();
    void sleep() { ... }  // Shared implementation
}

INTERFACE:
✓ CAN-DO relationship (Bird CAN fly, Dog CAN swim)
✓ Define BEHAVIOR contract (can include default implementations)
✓ No state (no fields, except constants)
✓ Multiple implementation allowed!

Example:
interface Flyable { void fly(); }
interface Swimmable { void swim(); }

class Duck implements Flyable, Swimmable {  // Multiple!
    public void fly() { ... }
    public void swim() { ... }
}