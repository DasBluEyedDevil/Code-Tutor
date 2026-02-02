---
type: "THEORY"
title: "Method Overriding with @Override"
---

OVERRIDING = Replacing a parent method with your own version

class Animal {
    public void makeSound() {
        IO.println("Generic sound");
    }
}

class Dog extends Animal {
    @Override  // ANNOTATION - tells Java "I'm overriding"
    public void makeSound() {
        IO.println("Woof!");
    }
}

class Cat extends Animal {
    @Override
    public void makeSound() {
        IO.println("Meow!");
    }
}

RULES FOR OVERRIDING:
1. Same method name
2. Same parameters (signature)
3. Same or more accessible (public > protected > default > private)
4. Use @Override annotation (optional but HIGHLY recommended)