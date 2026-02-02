---
type: "THEORY"
title: "The Problem: Incomplete Parent Classes"
---

Remember our Animal example?

class Animal {
    public void makeSound() {
        IO.println("Generic sound");  // What IS a generic sound?
    }
}

Problems:
1. There's no such thing as a "generic animal" - it's too abstract
2. What if someone creates: Animal a = new Animal()?
3. What if a subclass FORGETS to override makeSound()?

We need a way to say:
- "Animal is a concept, not a real thing" (can't instantiate)
- "Every subclass MUST implement makeSound()" (enforce contract)

Solution: ABSTRACT CLASSES