---
type: "KEY_POINT"
title: "Dynamic Method Dispatch"
---

HOW does Java know which version to call?

Animal a = new Dog();
a.makeSound();  // Which makeSound()?

Java uses DYNAMIC DISPATCH (runtime polymorphism):
1. Looks at the ACTUAL object type (Dog)
2. Finds the method in Dog's class
3. Calls Dog's version, not Animal's

This happens at RUNTIME, not compile time!

KEY INSIGHT:
- Variable type (Animal) determines WHAT methods are available
- Object type (Dog) determines WHICH version gets called

This is the foundation of flexible, extensible code!