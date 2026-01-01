---
type: "WARNING"
title: "Common Polymorphism Pitfalls"
---

1. FORGETTING @Override ANNOTATION:
Without @Override, typos create NEW methods instead of overriding!

class Dog extends Animal {
    public void makesound() { ... }  // TYPO! New method, not override
}

With @Override, the compiler catches the error.

2. CALLING OVERRIDABLE METHODS IN CONSTRUCTORS:
Never call non-final methods from constructors - subclass fields are not initialized yet!

class Animal {
    Animal() { makeSound(); }  // DANGEROUS!
}

3. EXCESSIVE DOWNCASTING:
Avoid casting parent to child type - it defeats polymorphism:
Animal a = new Dog();
((Dog) a).fetch();  // BAD - redesign with polymorphism

4. REDUCING VISIBILITY:
Overriding methods cannot be MORE restrictive:
public in parent -> must be public in child

5. MODERN ALTERNATIVE - SEALED CLASSES (Java 17+):
For controlled hierarchies, consider sealed classes to restrict which classes can extend yours.