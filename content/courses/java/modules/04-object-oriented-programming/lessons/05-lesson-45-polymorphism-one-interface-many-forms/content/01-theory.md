---
type: "THEORY"
title: "The Problem: Different Animals Make Different Sounds"
---

You have an Animal class with a makeSound() method:

class Animal {
    void makeSound() {
        System.out.println("Generic animal sound");
    }
}

class Dog extends Animal {
    // Inherits makeSound(), but dogs don't make "generic animal sounds"!
}

class Cat extends Animal {
    // Same problem - cats meow, not generic sounds
}

We need a way for EACH subclass to have its OWN implementation
of makeSound() while still being treated as Animals.

Solution: METHOD OVERRIDING + POLYMORPHISM