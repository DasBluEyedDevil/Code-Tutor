---
type: "KEY_POINT"
title: "Inheritance is Like Family Traits"
---

BIOLOGICAL INHERITANCE:
- Your parents have: eyes, hair, height genes
- YOU inherit: eyes, hair, height from them
- You also have: your own unique features

JAVA INHERITANCE:
class Animal {  // PARENT (superclass)
    String name;
    void eat() { ... }
    void sleep() { ... }
}

class Dog extends Animal {  // CHILD (subclass)
    // Automatically has: name, eat(), sleep()
    void bark() { ... }  // PLUS its own method
}

Dog d = new Dog();
d.eat();  // From Animal
d.sleep();  // From Animal
d.bark();  // Dog's own method